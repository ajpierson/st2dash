/*
 * Created by SharpDevelop.
 * User: Paul
 * Date: 27.06.2015
 * Time: 13:03
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 *
 *  2015.10.30.18.50   Neal Conrardy      - Updated and converted to English.
 * 
 */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Xml;

namespace st2dash
{
  /// <summary>
  /// Description of MainForm.
  /// </summary>
  public partial class MainForm : Form
  {
    public MainForm()
    {
      // The InitializeComponent() call is required for Windows Forms designer support.

      InitializeComponent();

      // TODO: Add constructor code after the InitializeComponent() call.
    }

    private void ButtonClickInputFile(object sender, EventArgs e)
    {
      OpenFileDialog flightlog = new OpenFileDialog();

      flightlog.Filter = "csv Files (*.csv)|*.csv";

      if (flightlog.ShowDialog() == DialogResult.OK)
      {
        textBoxInputFilename.Text = flightlog.FileName;

        if (textBoxOutputFilename.Text.Length > 0)
        {
          buttonStart.Enabled = true;
        }
      }
    }

    private void ButtonClickOutputFile(object sender, EventArgs e)
    {
      SaveFileDialog flightlog_dst = new SaveFileDialog();

      flightlog_dst.Filter = "csv Files (*.csv)|*.csv";

      if (flightlog_dst.ShowDialog() == DialogResult.OK)
      {
        textBoxOutputFilename.Text = flightlog_dst.FileName;

        if (textBoxInputFilename.Text.Length > 0)
        {
          buttonStart.Enabled = true;
        }
      }
    }

    private void ButtonClickStart(object sender, EventArgs e)
    {
      string   height_xml      ;
      string   myline          ;
      string   datetime        ;
      string[] mycol           ;

      int    count        = 0  ;
      int    skip         = 0  ;
      double sealevel     = 0.0;
      double altitude     = 0.0;
      double first_lat    = 0.0;
      double first_lng    = 0.0;
      double dist_f_start = 0.0;
      double lat          = 0.0;
      double lng          = 0.0;
      double avg_lat      = 0.0;
      double avg_lng      = 0.0;

    //toolStripStatusLabel1.Text = "Prüfe Quelldatei";
      toolStripStatusLabel1.Text = "Check source file";
      toolStripProgressBar1.Value = 25;
      statusStrip1.Update();

      StreamReader infile = new StreamReader(textBoxInputFilename.Text);

      // First pass, just check the file for correct column count and calculate average latitude / longitude      

      while ((myline = infile.ReadLine()) != null)
      {
        count++;
        if (count == 1)
        {
          continue;
        }

        mycol = myline.Split(',');

        if (mycol.Length != 24)
        {
        //MessageBox.Show("Falsche Anzahl an Spalten im Flightlog auf Zeile:" + count.ToString());
          MessageBox.Show("Wrong number of columns in Flightlog on line:" + count.ToString());
          Environment.Exit(1);
        }

        lat = Double.Parse(mycol[5], CultureInfo.InvariantCulture);
        lng = Double.Parse(mycol[6], CultureInfo.InvariantCulture);

        if (lat == 0 && lng == 0)
        {
          count--;
          continue;
        }

        avg_lat += lat;
        avg_lng += lng;
      }

      avg_lat = avg_lat / count;
      avg_lng = avg_lng / count;

      infile.DiscardBufferedData();
      infile.BaseStream.Seek(0, SeekOrigin.Begin);

      count = 0;

      StreamWriter outfile = new StreamWriter(textBoxOutputFilename.Text);

      // Second pass, now convert we already know the file is correct and we have average values to trow out lines with coordinates that far
      //  of all the points during the flight.

      while ((myline = infile.ReadLine()) != null)
      {
        count++;
        mycol = myline.Split(',');

        if (count == 1)
        {
          outfile.WriteLine("distance_from_start,Datetime,altitude" + myline.Replace("altitude", "ascent"));
          continue;
        }

        if (count == 2)
        {
          first_lat = lat = Double.Parse(mycol[5], CultureInfo.InvariantCulture);
          first_lng = lng = Double.Parse(mycol[6], CultureInfo.InvariantCulture);
        }
        else
        {
          lat = Double.Parse(mycol[5], CultureInfo.InvariantCulture);
          lng = Double.Parse(mycol[6], CultureInfo.InvariantCulture);
        }

        dist_f_start = Math.Acos(Math.Sin(first_lat * Math.PI / 180) * Math.Sin(lat * Math.PI / 180) +
                                 Math.Cos(first_lat * Math.PI / 180) * Math.Cos(lat * Math.PI / 180) *
                                 Math.Cos(lng * Math.PI / 180 - first_lng * Math.PI / 180)) * 6371000;

        if (lat == 0 && lng == 0)
        {
          skip++;
          count--;
          continue;
        }

        if (Difference(avg_lat, lat) > int.Parse(textBoxMaxLatLongDeviation.Text) || Difference(avg_lng, lng) > int.Parse(textBoxMaxLatLongDeviation.Text))
        {
          skip++;
          count--;
          continue;
        }

        if (count == 2 && checkBoxEarthTools.Checked)
        {
        //toolStripStatusLabel1.Text = "Ermittle Meereshöhe von GPS Startpunkt";
          toolStripStatusLabel1.Text = "Determine elevation of GPS starting point";
          toolStripProgressBar1.Value = 50;
          statusStrip1.Update();

          using (WebClient client = new WebClient())
          {
            height_xml = client.DownloadString("http://new.earthtools.org/height/" + first_lat.ToString("G", CultureInfo.InvariantCulture) + "/" + first_lng.ToString("G", CultureInfo.InvariantCulture));
          }

          try
          {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(height_xml);
            sealevel = Double.Parse(xmlDoc.GetElementsByTagName("meters")[0].ChildNodes[0].Value, CultureInfo.InvariantCulture);
          }
          catch
          {
          //MessageBox.Show("Konnte Meereshöhe nicht ermitteln, benutze Meereshöhe 0");
            MessageBox.Show("Could not determine altitude, using elevation 0.0");
            sealevel = 0.0;
          }
        }

        if (count == 3)
        {
        //toolStripStatusLabel1.Text = "Konvertiere Flightlog";
          toolStripStatusLabel1.Text = "Converting flight log";
          toolStripProgressBar1.Value = 75;
          statusStrip1.Update();
        }

        datetime = mycol[0].Substring(0, 4) + "-" + mycol[0].Substring(4, 2) + "-" + mycol[0].Substring(6, 2) + " " + mycol[0].Substring(9, 8) + "." + mycol[0].Substring(18, 3);
        mycol = myline.Split(',').Skip(1).ToArray();
        myline = String.Join(",", mycol);
        altitude = sealevel + Double.Parse(mycol[3], CultureInfo.InvariantCulture);
        outfile.WriteLine(dist_f_start.ToString("G", CultureInfo.InvariantCulture) + "," + datetime + "," + altitude.ToString("G", CultureInfo.InvariantCulture) + "," + myline);
      }
      outfile.Close();
      infile.Close();
    //toolStripStatusLabel1.Text = "Beendet.";
      toolStripStatusLabel1.Text = "Finished.";
      toolStripProgressBar1.Value = 100;
      statusStrip1.Update();
    //MessageBox.Show("Flightlog erfolgreich konvertiert.\n\n" + skip + " Zeilen mit Längen-/Breitengraden außerhalb des Bereichs verworfen.\n" + count + " Zeilem erfolgreich konvertiert\n" + (skip + count) + " Zeilen wurden insgesamt bearbeitet.");
      MessageBox.Show("Flight log successfully converted.\n\n" + skip + " Rows rejected with lat/long is out of range.\n" + count + " Successfully converted lines.\n" + (skip + count) + " rows were processed in total.");
    }

    void CancelClick(object sender, EventArgs e)
    {
      Environment.Exit(0);
    }
    
    private void ButtonClickHelp(object sender, EventArgs e)
    {
    //MessageBox.Show("Dieses Programm konvertiert ein mit der ST10+ Fernsteuereug erzeugtes Flightlog in eine unter Dashware benutzbare Flightlog Datei. \n\n" +
    //                "Als Quelldatei die Orignal Datei aus der ST10+ wählen.\n Als Zieldatei den Namen für das konvertierte Flightlog wählen.\n" +
    //                "Die Angabe bei Längen-/Breitengrad Abweichung ist die maximale Gradzahl welche alle Flugpunkte vom Mittelpunkt der Flugstrecke abweichen dürfen. " +
    //                "(Je höher der Wert, desto tolleranter werden Längen-/Breitengrad Abweichungen akzeptiert.)\n" +
    //                "Da die ST10+ keine Höhe über dem Meeresspiegel misst, kann mit Hilfe von earthools.org versucht werden die Meereshöhe über die GPS Daten zu ermitteln.\n" +
    //                "Wenn dies nicht erwünscht ist, die Checkbox deaktivieren, dann wird die Flughöhe gleichgesetzt mit dem Höhe über dem Startpunkt.\n\n" +
    //                "Auf Start klicken -> fertig.");
      MessageBox.Show("This program converts an flight log file from the  ST10+ remote control into a compatible Dashware flight log file.\n\n" +
                      "Select a ST10+ flight log file and the elect the name for the compatible Dashware output file.\n" +
                      "The specification for lat/long deviation is the maximum number of degrees which all flight points may differ from the center of the route." +
                      "(The higher the value, the more tolerant latitude/longitude are accepted deviations.)\n" +
                      "The ST10+ does not measure altitude above sea level so the user can use earthools.org to it given the GPS location.\n" +
                      "If this is not desired, disable the checkbox, the altitude is then set equal to the height above the starting point .\n\n" +
                      "Click Start -> Close.");
    }

    public double Difference(double first, double second)
    {
      return Math.Abs(first - second);
    }

    void ToolStripStatusLabel1Click(object sender, EventArgs e)
    {

    }

    private void textBox2_TextChanged(object sender, EventArgs e)
    {

    }

    private void checkBox1_CheckedChanged(object sender, EventArgs e)
    {

    }

    private void textBox3_TextChanged(object sender, EventArgs e)
    {

    }
  }
}
