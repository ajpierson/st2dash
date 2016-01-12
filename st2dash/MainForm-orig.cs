/*
 * Created by SharpDevelop.
 * User: Paul
 * Date: 27.06.2015
 * Time: 13:03
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Globalization;
using System.IO;
using System.Linq;

namespace st2dash
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		void Button1Click(object sender, EventArgs e)
		{
			OpenFileDialog flightlog = new OpenFileDialog();
			flightlog.Filter="csv Files (*.csv)|*.csv";
			if (flightlog.ShowDialog() == DialogResult.OK)
			{
				textBox1.Text=flightlog.FileName;
			}
			
		}
		void Button2Click(object sender, EventArgs e)
		{
			SaveFileDialog flightlog_dst = new SaveFileDialog();
			flightlog_dst.Filter="csv Files (*.csv)|*.csv";
			if (flightlog_dst.ShowDialog() == DialogResult.OK)
			{
				textBox2.Text=flightlog_dst.FileName;
			}
		}
		void Button3Click(object sender, EventArgs e)
		{
			int count = 0;
			int skip = 0;
			string myline;
			string datetime;
			string[] mycol;
			double time = 0;
			double t1 = 0;
			double first_time = 0;
			double first_lat = 0;
			double first_lng = 0;
			double dist_f_start = 0;
			double lat = 0;
			double lng = 0;
			double avg_lat = 0;
			double avg_lng = 0;
			
			StreamReader infile = new StreamReader(textBox1.Text);
			
			//First pass, just check the file for correct column count and
			//calculate average latitude / longitude			
			while ((myline = infile.ReadLine()) != null)
			{
				count++;
				if (count == 1) {
					continue;
				}
				mycol = myline.Split(',');
				if (mycol.Length != 24) 
				{
					MessageBox.Show("Falsche Anzahl an Spalten im Flightlog auf Zeile:" + count.ToString());
					Environment.Exit(1);
				}
				lat = Double.Parse(mycol[5],CultureInfo.InvariantCulture);
				lng = Double.Parse(mycol[6],CultureInfo.InvariantCulture);
				if (lat == 0 && lng == 0)
					{
						count--;
						continue;
					}
				avg_lat += lat;
				avg_lng += lng;
			}
			
			avg_lat = avg_lat/count;
			avg_lng = avg_lng/count;
			infile.DiscardBufferedData();
			infile.BaseStream.Seek(0, SeekOrigin.Begin);
			count = 0;
						
			StreamWriter outfile = new StreamWriter(textBox2.Text);
			
			//second pass, now convert we already know the file is correct and
			//we have average values to trow out lines with coordinates that far
			//of all the points during the flight.
			while ((myline = infile.ReadLine()) != null)
			{
				count++;
				mycol = myline.Split(',');
				
				if (count == 1)
				{
					outfile.WriteLine("time,t1,first_time,first_lat,first_long,distance_from_start,Datetime" + myline);
					continue;
				}
				if (count == 2)
				{
					first_time = Double.Parse(mycol[0].Substring(18,3))/1000+Double.Parse(mycol[0].Substring(15,2))+(Double.Parse(mycol[0].Substring(12,2))*60)+(Double.Parse(mycol[0].Substring(9,2))*60*60);
					first_lat = lat = Double.Parse(mycol[5],CultureInfo.InvariantCulture);
					first_lng = lng = Double.Parse(mycol[6],CultureInfo.InvariantCulture);
				} else {
					t1 = Double.Parse(mycol[0].Substring(18,3))/1000+Double.Parse(mycol[0].Substring(15,2))+(Double.Parse(mycol[0].Substring(12,2))*60)+(Double.Parse(mycol[0].Substring(9,2))*60*60);
					time = t1-first_time;
					lat = Double.Parse(mycol[5],CultureInfo.InvariantCulture);
					lng = Double.Parse(mycol[6],CultureInfo.InvariantCulture);
				}
				dist_f_start = Math.Acos(Math.Sin(first_lat*Math.PI/180)*Math.Sin(lat*Math.PI/180)+Math.Cos(first_lat*Math.PI/180)*Math.Cos(lat*Math.PI/180)*Math.Cos(lng*Math.PI/180-first_lng*Math.PI/180))*6371000;
				if (lat == 0 && lng == 0)
				{
					skip++;
					count--;
					continue;
				}
				if (Difference(avg_lat,lat) > int.Parse(textBox3.Text) || Difference(avg_lng,lng) > int.Parse(textBox3.Text))
				{
					skip++;
					count--;
					continue;
				}
				datetime = mycol[0].Substring(0,4)+"-"+mycol[0].Substring(4,2)+"-"+mycol[0].Substring(6,2)+" "+mycol[0].Substring(9,8)+"."+mycol[0].Substring(18,3);
				mycol = myline.Split(',').Skip(1).ToArray();
				myline = String.Join(",",mycol);
				outfile.WriteLine(time.ToString("G",CultureInfo.InvariantCulture)+","+t1.ToString("G",CultureInfo.InvariantCulture)+
				                  ","+first_time.ToString("G",CultureInfo.InvariantCulture)+","+first_lat.ToString("G",CultureInfo.InvariantCulture)+
				                  ","+first_lng.ToString("G",CultureInfo.InvariantCulture)+","+dist_f_start.ToString("G",CultureInfo.InvariantCulture)+
				                  ","+datetime+","+myline);
			}
			outfile.Close();
			infile.Close();
			MessageBox.Show("Flightlog erfolgreich konvertiert.\n\n"+skip+" Zeilen mit Längen-/Breitengraden außerhalb des Bereichs verworfen.\n"+count+" Zeilem erfolgreich konvertiert\n"+(skip+count)+" Zeilen wurden insgesamt bearbeitet.");
		}
		void CancelClick(object sender, EventArgs e)
		{
			Environment.Exit(0);
		}
		void Button4Click(object sender, EventArgs e)
		{
			MessageBox.Show("Dieses Programm konvertiert ein mit der ST10+ Fernsteuereug erzeugtes Flightlog in eine unter Dashware benutzbare Flightlog Datei. \n\n" +
			                "Als Quelldatei die Orignal Datei aus der ST10+ wählen.\nAls Zieldatei den Namen für das konvertierte Flightlog wählen.\n" +
			                "Die Angabe bei Längen-/Breitengrad Abweichung ist die maximale Gradzahl welche alle Flugpunkte vom Mittelpunkt der Flugstrecke abweichen dürfen. " +
			      			"(Je höher der Wert, desto tolleranter werden Längen-/Breitengrad Abweichungen akzeptiert.)\n" +
			                "Auf Start klicken -> fertig.");
		}
		public double Difference(double first, double second)
		{
			return Math.Abs(first - second);
		}

	}
}
