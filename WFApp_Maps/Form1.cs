using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;

namespace WFApp_Maps
{
    public partial class Form1 : Form
    {
        GMarkerGoogle marker;
        GMapOverlay markerOverlay;
        //DataTable dt;
        //int filaSeleccionada = 0;
        double LatInicial = 42.2144462858242;
        double LngInicial = -8.64036083221436;

        Data_connection dbobject = new Data_connection();
        SQLiteConnection SQLconnect = new SQLiteConnection();
        //private Color actualpen;

        public Form1()
        {
            InitializeComponent();

            //Iniciar maximizado
            this.WindowState = FormWindowState.Maximized;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            gMapControl1.DragButton = MouseButtons.Left;
            gMapControl1.CanDragMap = true;
            gMapControl1.MapProvider = GMapProviders.GoogleSatelliteMap;
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerAndCache;
            gMapControl1.Position = new PointLatLng(LatInicial, LngInicial);
            gMapControl1.Bearing = -30;
            gMapControl1.MinZoom = 0;
            gMapControl1.MaxZoom = 21;
            gMapControl1.Zoom = 17;
            gMapControl1.AutoScroll = true;
            gMapControl1.ShowCenter = false;

            ////Marcador
            //markerOverlay = new GMapOverlay("Marcador");
            //marker = new GMarkerGoogle(new PointLatLng(LatInicial, LngInicial), GMarkerGoogleType.green);
            //markerOverlay.Markers.Add(marker); // Agregamos al mapa

            //// Agregamos el marcador al map control
            //gMapControl1.Overlays.Add(markerOverlay);

            DibujarContornoCM();
            DibujarContorno93();
            DibujarContorno94();
            DibujarContorno352();
            DibujarContorno95();
            DibujarContorno96();
            DibujarContorno97();
            DibujarContorno98();
            DibujarContorno13();
            DibujarContorno110();
            DibujarContorno111();
            DibujarContorno112();
            DibujarContorno113();
            DibujarContorno322();
            DibujarContorno114();
            DibujarContorno115();
            DibujarContorno361();
            DibujarContorno362();
            DibujarContorno363();
            DibujarContorno101();
            DibujarContorno6007();
            DibujarContorno338();
            DibujarContorno14();
            DibujarContorno15();
            DibujarContorno104();
            DibujarContorno350();
            DibujarContorno6004();
            DibujarContorno12();
            DibujarContorno63();
            DibujarContorno62();
            DibujarContorno61();
            DibujarContorno36();
            DibujarContorno292();
            DibujarContorno35();
            DibujarContorno289();
            DibujarContorno290();
            DibujarContorno32217();
            DibujarContorno178();
            DibujarContorno1038();
            DibujarContorno16();
            DibujarContorno321();
            DibujarContorno103();
            DibujarContorno360();
            DibujarContorno100();
            DibujarContorno105();
            DibujarContorno106();
            DibujarContorno107();
            DibujarContorno108();
            DibujarContorno109();
            DibujarContorno342();
            DibujarContorno337();
            DibujarContorno339();
        }

        private void gMapControl1_MouseClick(object sender, MouseEventArgs e)
        {
            ////quitar comentarios para que muestre las coordenadas en los txtbox
            ////los txtbox están ocultos, hay que mostrarlos

            //double lat = gMapControl1.FromLocalToLatLng(e.X, e.Y).Lat;
            //double lng = gMapControl1.FromLocalToLatLng(e.X, e.Y).Lng;
            //// Mostrar texto lat lng en los txtbox
            //txtLatitud.Text = lat.ToString();
            //txtLongitud.Text = lng.ToString();
            //// Creamos el marcador para moverlo al lugar indicado
            //marker.Position = new PointLatLng(lat, lng);
            //// Cambiamos el texto del marcador(tooltip)
            //marker.ToolTipText = string.Format("Ubicación: \n Latitud: {0} \n Longitud; {1}", lat, lng);
        }

        public void gMapControl1_OnPolygonEnter(GMapPolygon item)
        {
            label1.BringToFront();
            label1.Show();
            label1.Text = "Parcela: " + item.Name.ToString();
            label1.Location = new Point(MousePosition.X, MousePosition.Y);

            //Cambia el borde del poligono al entrar //descartado porque al mover rapido el raton cambia varios a la vez
            //actualpen = item.Stroke.Color;
            //item.Stroke = new Pen(Color.White, 2);
        }

        public void gMapControl1_OnPolygonLeave(GMapPolygon item)
        {
            label1.Hide();
            //item.Stroke = new Pen(actualpen);
        }

        private void gMapControl1_OnPolygonClick(GMapPolygon item, MouseEventArgs e)
        {
            SQLiteConnection SQLconnect = new SQLiteConnection();

            SQLconnect.ConnectionString = dbobject.datalocation();
            SQLconnect.Open();
            SQLiteCommand comm = new SQLiteCommand("Select * From deslinde where n_plano_catastro = " + item.Name.ToString(), SQLconnect);
            using (SQLiteDataReader read = comm.ExecuteReader())

            {
                while (read.Read())
                {

                    tbNPlanoCatastro.Text = read.GetValue(read.GetOrdinal("n_plano_catastro")).ToString();
                    tbRefCatTerreno.Text = read.GetValue(read.GetOrdinal("ref_cata_1")).ToString();
                    tbRefCatVivienda.Text = read.GetValue(read.GetOrdinal("ref_cata_2")).ToString();
                    tbPropietario.Text = read.GetValue(read.GetOrdinal("propietario")).ToString();
                    tbDireccion.Text = read.GetValue(read.GetOrdinal("direccion")).ToString();
                    tbTelefono.Text = read.GetValue(read.GetOrdinal("telefono")).ToString();
                    tbAnoDoc.Text = read.GetValue(read.GetOrdinal("ano_doc")).ToString();
                    tbDocOK.Text = read.GetValue(read.GetOrdinal("doc_ok")).ToString();
                    if (tbDocOK.Text != "0")
                    {
                        checkBox1.Checked = true;
                        pictureBox2.Visible = true;
                    }
                    else
                    {
                        checkBox1.Checked = false;
                        pictureBox2.Visible = false;
                    }

                    rtbComentarios.Text = read.GetValue(read.GetOrdinal("comentarios")).ToString();
                    //dataGridView1.Rows.Add(new object[] {
                    //    //read.GetValue(0),  // U can use column index
                    //    read.GetValue(read.GetOrdinal("ID")),  // Or column name like this
                    //    read.GetValue(read.GetOrdinal("n_plano_catastro")),
                    //    read.GetValue(read.GetOrdinal("ref_cata_1")),
                    //    read.GetValue(read.GetOrdinal("ref_cata_2")),
                    //    read.GetValue(read.GetOrdinal("ref_cata_3")),
                    //    read.GetValue(read.GetOrdinal("propietario")),
                    //    read.GetValue(read.GetOrdinal("direccion")),
                    //    read.GetValue(read.GetOrdinal("telefono")),
                    //    read.GetValue(read.GetOrdinal("ano_doc")),
                    //    read.GetValue(read.GetOrdinal("doc_ok")),
                    //    read.GetValue(read.GetOrdinal("comentarios"))
                };
            }

            SQLconnect.Close();
        }


        #region Definir parcelas

        private void DibujarContornoCM()
        {
            GMapOverlay PoligonoCM = new GMapOverlay("Poligono_CM");
            List<PointLatLng> puntos = new List<PointLatLng>();
            puntos.Add(new PointLatLng(42.2150501906204, -8.64652186632156));
            puntos.Add(new PointLatLng(42.2156143596206, -8.64514321088791));
            puntos.Add(new PointLatLng(42.215991793814, -8.64386111497879));
            puntos.Add(new PointLatLng(42.2169174912851, -8.64195942878723));
            puntos.Add(new PointLatLng(42.2168042629237, -8.64196747541428));
            puntos.Add(new PointLatLng(42.216738709569, -8.64203453063965));
            puntos.Add(new PointLatLng(42.2165639002905, -8.64241272211075));
            puntos.Add(new PointLatLng(42.2164248471098, -8.6425307393074));
            puntos.Add(new PointLatLng(42.216134820919, -8.64306449890137));
            puntos.Add(new PointLatLng(42.2159560369872, -8.643348813056956));
            puntos.Add(new PointLatLng(42.2158229419537, -8.64324420690537));
            puntos.Add(new PointLatLng(42.2161944154505, - 8.64249050617218));
            puntos.Add(new PointLatLng(42.216659250866, - 8.6407470703125));
            puntos.Add(new PointLatLng(42.2169055725198, - 8.64043056964874));
            puntos.Add(new PointLatLng(42.2170922995848, - 8.64048957824707));
            puntos.Add(new PointLatLng(42.2171598390256, - 8.64066660404205));
            puntos.Add(new PointLatLng(42.2171399744917, - 8.64091873168945));
            puntos.Add(new PointLatLng(42.2173346466544, - 8.64112257957459));
            puntos.Add(new PointLatLng(42.2177994736771, - 8.64024817943573));
            puntos.Add(new PointLatLng(42.2174061587268, - 8.63987267017365));
            puntos.Add(new PointLatLng(42.2178233108681, - 8.63932013511658));
            puntos.Add(new PointLatLng(42.2184430746754, - 8.63807559013367));
            puntos.Add(new PointLatLng(42.2183000527982, - 8.63801658153534));
            puntos.Add(new PointLatLng(42.2178868766667, - 8.63888025283813));
            puntos.Add(new PointLatLng(42.2177279620502, - 8.63876223564148));
            puntos.Add(new PointLatLng(42.2174776707181, - 8.63871395587921));
            puntos.Add(new PointLatLng(42.2173068363822, - 8.63851010799408));
            puntos.Add(new PointLatLng(42.2167585742291, - 8.63815605640411));
            puntos.Add(new PointLatLng(42.2167307637032, - 8.63790392875671));
            puntos.Add(new PointLatLng(42.2169492746483, - 8.63737285137177));
            puntos.Add(new PointLatLng(42.2166314402964, - 8.63716900348663));
            puntos.Add(new PointLatLng(42.2164923872644, - 8.63751769065857));
            puntos.Add(new PointLatLng(42.2161467398298, - 8.6380273103714));
            puntos.Add(new PointLatLng(42.2157931445197, - 8.63735675811768));
            puntos.Add(new PointLatLng(42.215598467605, - 8.63751769065857));
            puntos.Add(new PointLatLng(42.215538872511, - 8.63729238510132));
            puntos.Add(new PointLatLng(42.215383925004, - 8.63737821578979));
            puntos.Add(new PointLatLng(42.2153263162185, - 8.63738626241684));
            puntos.Add(new PointLatLng(42.2149449050025, - 8.63640993833542));
            puntos.Add(new PointLatLng(42.2148912688655, - 8.63646894693375));
            puntos.Add(new PointLatLng(42.2148197539453, - 8.63668888807297));
            puntos.Add(new PointLatLng(42.2147522120019, - 8.63731116056442));
            puntos.Add(new PointLatLng(42.2146707642681, - 8.63761425018311));
            puntos.Add(new PointLatLng(42.2149866219664, - 8.63733261823654));
            puntos.Add(new PointLatLng(42.2152905590148, - 8.6377027630806));
            puntos.Add(new PointLatLng(42.2155587375487, - 8.63828212022781));
            puntos.Add(new PointLatLng(42.215411736123, - 8.63894194364548));
            puntos.Add(new PointLatLng(42.2149389454339, - 8.63956555724144));
            puntos.Add(new PointLatLng(42.2147313534459, - 8.63943949341774));
            puntos.Add(new PointLatLng(42.2143519251782, - 8.63989546895027));
            puntos.Add(new PointLatLng(42.2142029343974, - 8.64023476839066));
            puntos.Add(new PointLatLng(42.2141065868387, - 8.64075511693954));
            puntos.Add(new PointLatLng(42.2137797986635, - 8.64153161644936));
            puntos.Add(new PointLatLng(42.2137827785025, - 8.64168584346771));
            puntos.Add(new PointLatLng(42.2135046595878, - 8.64227458834648));
            puntos.Add(new PointLatLng(42.213326861497, - 8.64245429635048));
            puntos.Add(new PointLatLng(42.2132424320608, - 8.64263936877251));
            puntos.Add(new PointLatLng(42.213080525885, - 8.64275872707367));
            puntos.Add(new PointLatLng(42.2123246271425, - 8.64324688911438));
            puntos.Add(new PointLatLng(42.2118955189689, - 8.64346146583557));
            puntos.Add(new PointLatLng(42.2116253382529, - 8.64352583885193));
            puntos.Add(new PointLatLng(42.211347209838, - 8.64349365234375));
            puntos.Add(new PointLatLng(42.2102585239779, - 8.64312887191772));
            puntos.Add(new PointLatLng(42.2096784136303, - 8.64335417747498));
            puntos.Add(new PointLatLng(42.2113789960046, - 8.64448070526123));
            puntos.Add(new PointLatLng(42.2118220140356, - 8.64530146121979));
            puntos.Add(new PointLatLng(42.212123979702, - 8.64506274461746));
            puntos.Add(new PointLatLng(42.2123325735626, - 8.64482671022415));
            puntos.Add(new PointLatLng(42.2125908316738, - 8.64442437887192));
            puntos.Add(new PointLatLng(42.2136437191982, - 8.64470601081848));
            puntos.Add(new PointLatLng(42.2142237931279, - 8.64510297775269));
            puntos.Add(new PointLatLng(42.2142555778468, - 8.64529609680176));
            puntos.Add(new PointLatLng(42.2143588780727, - 8.64552140235901));
            puntos.Add(new PointLatLng(42.2141046002896, - 8.64572525024414));

            GMapPolygon poligonoPuntosCM = new GMapPolygon(puntos, "Polígono");
            PoligonoCM.Polygons.Add(poligonoPuntosCM);
            gMapControl1.Overlays.Add(PoligonoCM);
            //actualizar mapa
            //gMapControl1.Zoom += 1;
            //gMapControl1.Zoom -= 1;
            poligonoPuntosCM.Fill = new SolidBrush(Color.FromArgb(30, Color.White));
            poligonoPuntosCM.Stroke = new Pen(Color.White, 3);

        }

        private void DibujarContorno93()
        {
            GMapOverlay Poligono_parcelas = new GMapOverlay("Poligono 93");
            List<PointLatLng> puntos = new List<PointLatLng>();
            //puntos.Add(new PointLatLng(42.2150501906204, -8.64652186632156));
            puntos.Add(new PointLatLng(42.2147114881481, - 8.64284053444862));
            puntos.Add(new PointLatLng(42.2151991794024, - 8.64306181669235));
            puntos.Add(new PointLatLng(42.2152935387826, - 8.64307925105095));
            puntos.Add(new PointLatLng(42.2153710126946, - 8.64296928048134));
            puntos.Add(new PointLatLng(42.215381938495, - 8.64284589886665));
            puntos.Add(new PointLatLng(42.2151803075095, - 8.64273324608803));
            puntos.Add(new PointLatLng(42.2150978670689, - 8.64283382892609));
            puntos.Add(new PointLatLng(42.214813794365, - 8.64262863993645));

            GMapPolygon poligonoPuntos93 = new GMapPolygon(puntos, "93");
            Poligono_parcelas.Polygons.Add(poligonoPuntos93);
            gMapControl1.Overlays.Add(Poligono_parcelas);
            //actualizar mapa
            //gMapControl1.Zoom += 1;
            //gMapControl1.Zoom -= 1;
            poligonoPuntos93.Fill = new SolidBrush(Color.FromArgb(30, Color.Red));
            poligonoPuntos93.Stroke = new Pen(Color.Red, 2);
            poligonoPuntos93.IsHitTestVisible = true;
        }

        private void DibujarContorno94()
        {
            GMapOverlay Poligono_parcelas = new GMapOverlay("Poligono");
            List<PointLatLng> puntos = new List<PointLatLng>();
            //puntos.Add(new PointLatLng(42.2150501906204, -8.64652186632156));
            puntos.Add(new PointLatLng(42.2153585970099, -8.6428277939558));
            puntos.Add(new PointLatLng(42.2154008103277, - 8.64231750369072));
            puntos.Add(new PointLatLng(42.2152518220214, - 8.64228665828705));
            puntos.Add(new PointLatLng(42.2150819749234, - 8.64254280924797));
            puntos.Add(new PointLatLng(42.2152160647755, - 8.64261522889137));
            puntos.Add(new PointLatLng(42.2152081187182, - 8.64269971847534));
            puntos.Add(new PointLatLng(42.2151837839112, -8.64273257553577));

            GMapPolygon poligonoPuntos94 = new GMapPolygon(puntos, "94");
            Poligono_parcelas.Polygons.Add(poligonoPuntos94);
            gMapControl1.Overlays.Add(Poligono_parcelas);
            //actualizar mapa
            //gMapControl1.Zoom += 1;
            //gMapControl1.Zoom -= 1;
            poligonoPuntos94.Fill = new SolidBrush(Color.FromArgb(30, Color.Red));
            poligonoPuntos94.Stroke = new Pen(Color.Red, 2);
            poligonoPuntos94.IsHitTestVisible = true;
        }

        private void DibujarContorno352()
        // Con propietario anterior al catastro 1967
        {
            GMapOverlay Poligono_parcelas = new GMapOverlay("Poligono");
            List<PointLatLng> puntos = new List<PointLatLng>();
            //puntos.Add(new PointLatLng(42.2150501906204, -8.64652186632156));
            puntos.Add(new PointLatLng(42.2154003137006, - 8.64231549203396));
            puntos.Add(new PointLatLng(42.2154037900903, - 8.64226050674915));
            puntos.Add(new PointLatLng(42.2152567883039, - 8.64223100244999));
            puntos.Add(new PointLatLng(42.2150258557526, - 8.64227928221226));
            puntos.Add(new PointLatLng(42.2149185835706, - 8.64243887364864));
            puntos.Add(new PointLatLng(42.2151808041383, - 8.64273056387901));
            puntos.Add(new PointLatLng(42.2152061322037, - 8.64269904792309));
            puntos.Add(new PointLatLng(42.2152135816327, - 8.64261724054813));
            puntos.Add(new PointLatLng(42.215078498516, - 8.64254482090473));
            puntos.Add(new PointLatLng(42.2152503321365, - 8.64228397607803));

            GMapPolygon poligonoPuntos352 = new GMapPolygon(puntos, "352");
            Poligono_parcelas.Polygons.Add(poligonoPuntos352);
            gMapControl1.Overlays.Add(Poligono_parcelas);
            //actualizar mapa
            //gMapControl1.Zoom += 1;
            //gMapControl1.Zoom -= 1;
            poligonoPuntos352.Fill = new SolidBrush(Color.FromArgb(30, Color.Lime));
            poligonoPuntos352.Stroke = new Pen(Color.Lime, 2);
            poligonoPuntos352.IsHitTestVisible = true;
        }

        private void DibujarContorno95()
        {
            GMapOverlay Poligono_parcelas = new GMapOverlay("Poligono");
            List<PointLatLng> puntos = new List<PointLatLng>();
            //puntos.Add(new PointLatLng(42.2150501906204, -8.64652186632156));
            puntos.Add(new PointLatLng(42.2154047833444, - 8.64225581288338));
            puntos.Add(new PointLatLng(42.2154325944543, - 8.64213243126869));
            puntos.Add(new PointLatLng(42.2154226619164, - 8.64201575517654));
            puntos.Add(new PointLatLng(42.2152220243179, - 8.6419366300106));
            puntos.Add(new PointLatLng(42.2151326311232, - 8.64210292696953));
            puntos.Add(new PointLatLng(42.2150382715026, - 8.64223569631577));
            puntos.Add(new PointLatLng(42.2150273456427, - 8.64227190613747));
            puntos.Add(new PointLatLng(42.2152557950474, - 8.64222496747971));

            GMapPolygon poligonoPuntos95 = new GMapPolygon(puntos, "95");
            Poligono_parcelas.Polygons.Add(poligonoPuntos95);
            gMapControl1.Overlays.Add(Poligono_parcelas);
            //actualizar mapa
            //gMapControl1.Zoom += 1;
            //gMapControl1.Zoom -= 1;
            poligonoPuntos95.Fill = new SolidBrush(Color.FromArgb(30, Color.Red));
            poligonoPuntos95.Stroke = new Pen(Color.Red, 2);
            poligonoPuntos95.IsHitTestVisible = true;
        }

        private void DibujarContorno96()
        {
            GMapOverlay Poligono_parcelas = new GMapOverlay("Poligono");
            List<PointLatLng> puntos = new List<PointLatLng>();
            //puntos.Add(new PointLatLng(42.2150501906204, -8.64652186632156));
            puntos.Add(new PointLatLng(42.2154057765985, - 8.64200502634048));
            puntos.Add(new PointLatLng(42.2154067698527, - 8.64181995391846));
            puntos.Add(new PointLatLng(42.2152101052326, - 8.64181190729141));
            puntos.Add(new PointLatLng(42.2151991794024, - 8.64176630973816));
            puntos.Add(new PointLatLng(42.2151803075095, - 8.64177837967873));
            puntos.Add(new PointLatLng(42.2151644153847, - 8.64176496863365));
            puntos.Add(new PointLatLng(42.215148523256, - 8.64172741770744));
            puntos.Add(new PointLatLng(42.2151316378648, - 8.64164158701897));
            puntos.Add(new PointLatLng(42.2151604423529, - 8.64144176244736));
            puntos.Add(new PointLatLng(42.2150919075148, - 8.64145651459694));
            puntos.Add(new PointLatLng(42.2150948872919, - 8.64149004220963));
            puntos.Add(new PointLatLng(42.2150700558116, - 8.64158526062965));
            puntos.Add(new PointLatLng(42.2151077996578, - 8.64181458950043));
            puntos.Add(new PointLatLng(42.2151038266224, - 8.6419004201889));
            puntos.Add(new PointLatLng(42.2152230175749, - 8.64193394780159));

            GMapPolygon poligonoPuntos96 = new GMapPolygon(puntos, "96");
            Poligono_parcelas.Polygons.Add(poligonoPuntos96);
            gMapControl1.Overlays.Add(Poligono_parcelas);
            //actualizar mapa
            gMapControl1.Zoom += 1;
            gMapControl1.Zoom -= 1;
            poligonoPuntos96.Fill = new SolidBrush(Color.FromArgb(30, Color.Red));
            poligonoPuntos96.Stroke = new Pen(Color.Red, 2);
            poligonoPuntos96.IsHitTestVisible = true;
        }

        private void DibujarContorno97()
        {
            GMapOverlay Poligono_parcelas = new GMapOverlay("Poligono");
            List<PointLatLng> puntos = new List<PointLatLng>();
            //puntos.Add(new PointLatLng(42.2150501906204, -8.64652186632156));
            puntos.Add(new PointLatLng(42.2154057765985, - 8.64181593060493));
            puntos.Add(new PointLatLng(42.2154097496149, - 8.64141494035721));
            puntos.Add(new PointLatLng(42.2153253229631, - 8.64123657345772));
            puntos.Add(new PointLatLng(42.2152269906028, - 8.64140555262566));
            puntos.Add(new PointLatLng(42.2152071254609, - 8.64142164587975));
            puntos.Add(new PointLatLng(42.2152081187182, - 8.64166170358658));
            puntos.Add(new PointLatLng(42.2151942131153, - 8.64166975021362));
            puntos.Add(new PointLatLng(42.2152130850041, - 8.64180788397789));


            GMapPolygon poligonoPuntos97 = new GMapPolygon(puntos, "97");
            Poligono_parcelas.Polygons.Add(poligonoPuntos97);
            gMapControl1.Overlays.Add(Poligono_parcelas);
            //actualizar mapa
            //gMapControl1.Zoom += 1;
            //gMapControl1.Zoom -= 1;
            poligonoPuntos97.Fill = new SolidBrush(Color.FromArgb(30, Color.Red));
            poligonoPuntos97.Stroke = new Pen(Color.Red, 2);
            poligonoPuntos97.IsHitTestVisible = true;
        }

        private void DibujarContorno98()
        {
            GMapOverlay Poligono_parcelas = new GMapOverlay("Poligono");
            List<PointLatLng> puntos = new List<PointLatLng>();
            //puntos.Add(new PointLatLng(42.2150501906204, -8.64652186632156));
            puntos.Add(new PointLatLng(42.2153044645964, - 8.64120572805405));
            puntos.Add(new PointLatLng(42.215198186145, - 8.64136666059494));
            puntos.Add(new PointLatLng(42.2150909142557, - 8.64142835140228));
            puntos.Add(new PointLatLng(42.2150104602191, - 8.64096835255623));
            puntos.Add(new PointLatLng(42.215148523256, - 8.64084228873253));

            GMapPolygon poligonoPuntos98 = new GMapPolygon(puntos, "98");
            Poligono_parcelas.Polygons.Add(poligonoPuntos98);
            gMapControl1.Overlays.Add(Poligono_parcelas);
            //actualizar mapa
            //gMapControl1.Zoom += 1;
            //gMapControl1.Zoom -= 1;
            poligonoPuntos98.Fill = new SolidBrush(Color.FromArgb(30, Color.Red));
            poligonoPuntos98.Stroke = new Pen(Color.Red, 2);
            poligonoPuntos98.IsHitTestVisible = true;
        }

        private void DibujarContorno13()
        {
            GMapOverlay Poligono_parcelas = new GMapOverlay("Poligono");
            List<PointLatLng> puntos = new List<PointLatLng>();
            //puntos.Add(new PointLatLng(42.2150501906204, -8.64652186632156));
            puntos.Add(new PointLatLng(42.2151296513479, - 8.64079400897026));
            puntos.Add(new PointLatLng(42.215198186145, - 8.64076718688011));
            puntos.Add(new PointLatLng(42.2152081187182, - 8.64070281386375));
            puntos.Add(new PointLatLng(42.2152329501441, - 8.64063709974289));
            puntos.Add(new PointLatLng(42.2152091119754, - 8.64057540893555));
            puntos.Add(new PointLatLng(42.2150154265207, - 8.64052042365074));

            GMapPolygon poligonoPuntos13 = new GMapPolygon(puntos, "13");
            Poligono_parcelas.Polygons.Add(poligonoPuntos13);
            gMapControl1.Overlays.Add(Poligono_parcelas);
            //actualizar mapa
            //gMapControl1.Zoom += 1;
            //gMapControl1.Zoom -= 1;
            poligonoPuntos13.Fill = new SolidBrush(Color.FromArgb(30, Color.Red));
            poligonoPuntos13.Stroke = new Pen(Color.Red, 2);
            poligonoPuntos13.IsHitTestVisible = true;
        }

        private void DibujarContorno110()
        {
            GMapOverlay Poligono_parcelas = new GMapOverlay("Poligono");
            List<PointLatLng> puntos = new List<PointLatLng>();
            //puntos.Add(new PointLatLng(42.2150501906204, -8.64652186632156));
            puntos.Add(new PointLatLng(42.214709501618, - 8.64283919334412));
            puntos.Add(new PointLatLng(42.2148356461568, - 8.64257499575615));
            puntos.Add(new PointLatLng(42.2149170936779, - 8.6424395442009));
            puntos.Add(new PointLatLng(42.2147442658861, - 8.64225581288338));
            puntos.Add(new PointLatLng(42.2144591983227, - 8.64269033074379));
            puntos.Add(new PointLatLng(42.2146012356319, - 8.64278689026833));

            GMapPolygon poligonoPuntos110 = new GMapPolygon(puntos, "110");
            Poligono_parcelas.Polygons.Add(poligonoPuntos110);
            gMapControl1.Overlays.Add(Poligono_parcelas);
            //actualizar mapa
            //gMapControl1.Zoom += 1;
            //gMapControl1.Zoom -= 1;
            poligonoPuntos110.Fill = new SolidBrush(Color.FromArgb(30, Color.Red));
            poligonoPuntos110.Stroke = new Pen(Color.Red, 2);
            poligonoPuntos110.IsHitTestVisible = true;
        }

        private void DibujarContorno111()
        {
            GMapOverlay Poligono_parcelas = new GMapOverlay("Poligono");
            List<PointLatLng> puntos = new List<PointLatLng>();
            //puntos.Add(new PointLatLng(42.2150501906204, -8.64652186632156));
            puntos.Add(new PointLatLng(42.2144577084192, - 8.64268898963928));
            puntos.Add(new PointLatLng(42.2146702676353, - 8.64236377179623));
            puntos.Add(new PointLatLng(42.2145932894971, - 8.64217936992645));
            puntos.Add(new PointLatLng(42.214697582436, - 8.64201910793781));
            puntos.Add(new PointLatLng(42.2147099982506, - 8.64189103245735));
            puntos.Add(new PointLatLng(42.2146563619141, - 8.641736805439));
            puntos.Add(new PointLatLng(42.2141820756601, - 8.64240869879723));
            puntos.Add(new PointLatLng(42.214160720279, - 8.64252001047134));

            GMapPolygon poligonoPuntos111 = new GMapPolygon(puntos, "111");
            Poligono_parcelas.Polygons.Add(poligonoPuntos111);
            gMapControl1.Overlays.Add(Poligono_parcelas);
            //actualizar mapa
            //gMapControl1.Zoom += 1;
            //gMapControl1.Zoom -= 1;
            poligonoPuntos111.Fill = new SolidBrush(Color.FromArgb(30, Color.Red));
            poligonoPuntos111.Stroke = new Pen(Color.Red, 2);
            poligonoPuntos111.IsHitTestVisible = true;
        }

        private void DibujarContorno112()
        {
            GMapOverlay Poligono_parcelas = new GMapOverlay("Poligono");
            List<PointLatLng> puntos = new List<PointLatLng>();
            //puntos.Add(new PointLatLng(42.2150501906204, -8.64652186632156));
            puntos.Add(new PointLatLng(42.2141830689334, - 8.64240199327469));
            puntos.Add(new PointLatLng(42.2146548720152, - 8.64173412322998));
            puntos.Add(new PointLatLng(42.2145446194001, - 8.64143639802933));
            puntos.Add(new PointLatLng(42.2141413514387, - 8.64194735884666));
            puntos.Add(new PointLatLng(42.2142605442077, - 8.64217132329941));

            GMapPolygon poligonoPuntos112 = new GMapPolygon(puntos, "112");
            Poligono_parcelas.Polygons.Add(poligonoPuntos112);
            gMapControl1.Overlays.Add(Poligono_parcelas);
            //actualizar mapa
            //gMapControl1.Zoom += 1;
            //gMapControl1.Zoom -= 1;
            poligonoPuntos112.Fill = new SolidBrush(Color.FromArgb(30, Color.Red));
            poligonoPuntos112.Stroke = new Pen(Color.Red, 2);
            poligonoPuntos112.IsHitTestVisible = true;
        }

        private void DibujarContorno113()
        {
            GMapOverlay Poligono_parcelas = new GMapOverlay("Poligono");
            List<PointLatLng> puntos = new List<PointLatLng>();
            //puntos.Add(new PointLatLng(42.2150501906204, -8.64652186632156));
            puntos.Add(new PointLatLng(42.2137887381801, - 8.64272117614746));
            puntos.Add(new PointLatLng(42.2140509634389, - 8.64223435521126));
            puntos.Add(new PointLatLng(42.2141830689334, - 8.64203318953514));
            puntos.Add(new PointLatLng(42.2141383716166,- 8.64195004105568));
            puntos.Add(new PointLatLng(42.2138523080398, - 8.6416657269001));
            puntos.Add(new PointLatLng(42.2137579465063, - 8.64193126559258));
            puntos.Add(new PointLatLng(42.2137668860261, - 8.64199697971344));

            GMapPolygon poligonoPuntos113 = new GMapPolygon(puntos, "113");
            Poligono_parcelas.Polygons.Add(poligonoPuntos113);
            gMapControl1.Overlays.Add(Poligono_parcelas);
            //actualizar mapa
            //gMapControl1.Zoom += 1;
            //gMapControl1.Zoom -= 1;
            poligonoPuntos113.Fill = new SolidBrush(Color.FromArgb(30, Color.Red));
            poligonoPuntos113.Stroke = new Pen(Color.Red, 2);
            poligonoPuntos113.IsHitTestVisible = true;
        }

        private void DibujarContorno322()
        {
            GMapOverlay Poligono_parcelas = new GMapOverlay("Poligono");
            List<PointLatLng> puntos = new List<PointLatLng>();
            //puntos.Add(new PointLatLng(42.2150501906204, -8.64652186632156));
            puntos.Add(new PointLatLng(42.2137271548175, - 8.64201039075851));
            puntos.Add(new PointLatLng(42.2135106192917, - 8.64250928163528));
            puntos.Add(new PointLatLng(42.2137172220115, - 8.64278554916382));
            puntos.Add(new PointLatLng(42.2137569532263, - 8.64269971847534));

            GMapPolygon poligonoPuntos322 = new GMapPolygon(puntos, "322");
            Poligono_parcelas.Polygons.Add(poligonoPuntos322);
            gMapControl1.Overlays.Add(Poligono_parcelas);
            //actualizar mapa
            //gMapControl1.Zoom += 1;
            //gMapControl1.Zoom -= 1;
            poligonoPuntos322.Fill = new SolidBrush(Color.FromArgb(30, Color.Red));
            poligonoPuntos322.Stroke = new Pen(Color.Red, 2);
            poligonoPuntos322.IsHitTestVisible = true;
        }


        private void DibujarContorno114()
        {
            GMapOverlay Poligono_parcelas = new GMapOverlay("Poligono");
            List<PointLatLng> puntos = new List<PointLatLng>();
            //puntos.Add(new PointLatLng(42.2150501906204, -8.64652186632156));
            puntos.Add(new PointLatLng(42.2141383716166, - 8.64194333553314));
            puntos.Add(new PointLatLng(42.2143787434814, - 8.64163890480995));
            puntos.Add(new PointLatLng(42.2142535913023, - 8.64146858453751));
            puntos.Add(new PointLatLng(42.2140330844786, - 8.64122584462166));
            puntos.Add(new PointLatLng(42.2138533013184, - 8.64166170358658));

            GMapPolygon poligonoPuntos114 = new GMapPolygon(puntos, "114");
            Poligono_parcelas.Polygons.Add(poligonoPuntos114);
            gMapControl1.Overlays.Add(Poligono_parcelas);
            //actualizar mapa
            //gMapControl1.Zoom += 1;
            //gMapControl1.Zoom -= 1;
            poligonoPuntos114.Fill = new SolidBrush(Color.FromArgb(30, Color.Red));
            poligonoPuntos114.Stroke = new Pen(Color.Red, 2);
            poligonoPuntos114.IsHitTestVisible = true;
        }


        private void DibujarContorno115()
        {
            GMapOverlay Poligono_parcelas = new GMapOverlay("Poligono");
            List<PointLatLng> puntos = new List<PointLatLng>();
            //puntos.Add(new PointLatLng(42.2150501906204, -8.64652186632156));
            puntos.Add(new PointLatLng(42.2140360643057, - 8.64122182130814));
            puntos.Add(new PointLatLng(42.2142506114855, - 8.64145919680595));
            puntos.Add(new PointLatLng(42.2144303935148, - 8.64120170474052));
            puntos.Add(new PointLatLng(42.2145674645523, - 8.64116683602333));
            puntos.Add(new PointLatLng(42.2142724634722, - 8.64078193902969));
            puntos.Add(new PointLatLng(42.2141393648906, - 8.64098578691483));

            GMapPolygon poligonoPuntos115 = new GMapPolygon(puntos, "115");
            Poligono_parcelas.Polygons.Add(poligonoPuntos115);
            gMapControl1.Overlays.Add(Poligono_parcelas);
            //actualizar mapa
            //gMapControl1.Zoom += 1;
            //gMapControl1.Zoom -= 1;
            poligonoPuntos115.Fill = new SolidBrush(Color.FromArgb(30, Color.Red));
            poligonoPuntos115.Stroke = new Pen(Color.Red, 2);
            poligonoPuntos115.IsHitTestVisible = true;
        }


        private void DibujarContorno361()
        {
            GMapOverlay Poligono_parcelas = new GMapOverlay("Poligono");
            List<PointLatLng> puntos = new List<PointLatLng>();
            //puntos.Add(new PointLatLng(42.2150501906204, -8.64652186632156));
            puntos.Add(new PointLatLng(42.2142744500161, - 8.64077791571617));
            puntos.Add(new PointLatLng(42.2144045685034, - 8.64094689488411));
            puntos.Add(new PointLatLng(42.2145783904918,- 8.64059552550316));
            puntos.Add(new PointLatLng(42.2144313867842, - 8.64037290215492));

            GMapPolygon poligonoPuntos361 = new GMapPolygon(puntos, "361");
            Poligono_parcelas.Polygons.Add(poligonoPuntos361);
            gMapControl1.Overlays.Add(Poligono_parcelas);
            //actualizar mapa
            //gMapControl1.Zoom += 1;
            //gMapControl1.Zoom -= 1;
            poligonoPuntos361.Fill = new SolidBrush(Color.FromArgb(30, Color.Red));
            poligonoPuntos361.Stroke = new Pen(Color.Red, 2);
            poligonoPuntos361.IsHitTestVisible = true;
        }


        private void DibujarContorno362()
        {
            GMapOverlay Poligono_parcelas = new GMapOverlay("Poligono");
            List<PointLatLng> puntos = new List<PointLatLng>();
            //puntos.Add(new PointLatLng(42.2150501906204, -8.64652186632156));
            puntos.Add(new PointLatLng(42.2144343665925, - 8.64036619663239));
            puntos.Add(new PointLatLng(42.2145853433614, - 8.64059418439865));
            puntos.Add(new PointLatLng(42.2145694510869, - 8.64063173532486));
            puntos.Add(new PointLatLng(42.2146389797582, - 8.64077657461166));
            puntos.Add(new PointLatLng(42.2147164544732, - 8.64088118076324));
            puntos.Add(new PointLatLng(42.2148475653128, - 8.64064782857895));
            puntos.Add(new PointLatLng(42.2145078684856, - 8.64020258188248));

            GMapPolygon poligonoPuntos362 = new GMapPolygon(puntos, "362");
            Poligono_parcelas.Polygons.Add(poligonoPuntos362);
            gMapControl1.Overlays.Add(Poligono_parcelas);
            //actualizar mapa
            //gMapControl1.Zoom += 1;
            //gMapControl1.Zoom -= 1;
            poligonoPuntos362.Fill = new SolidBrush(Color.FromArgb(30, Color.Red));
            poligonoPuntos362.Stroke = new Pen(Color.Red, 2);
            poligonoPuntos362.IsHitTestVisible = true;
        }



        private void DibujarContorno363()
        {
            GMapOverlay Poligono_parcelas = new GMapOverlay("Poligono");
            List<PointLatLng> puntos = new List<PointLatLng>();
            //puntos.Add(new PointLatLng(42.2150501906204, -8.64652186632156));
            puntos.Add(new PointLatLng(42.2145098550221, - 8.64019989967346));
            puntos.Add(new PointLatLng(42.2148485585757, - 8.64064514636993));
            puntos.Add(new PointLatLng(42.2149518578317, - 8.64043459296227));
            puntos.Add(new PointLatLng(42.2147244005924, - 8.64023879170418));
            puntos.Add(new PointLatLng(42.214594282764, - 8.6400979757309));
            puntos.Add(new PointLatLng(42.2145823635602, - 8.64006578922272));

            GMapPolygon poligonoPuntos363 = new GMapPolygon(puntos, "363");
            Poligono_parcelas.Polygons.Add(poligonoPuntos363);
            gMapControl1.Overlays.Add(Poligono_parcelas);
            //actualizar mapa
            //gMapControl1.Zoom += 1;
            //gMapControl1.Zoom -= 1;
            poligonoPuntos363.Fill = new SolidBrush(Color.FromArgb(30, Color.Red));
            poligonoPuntos363.Stroke = new Pen(Color.Red, 2);
            poligonoPuntos363.IsHitTestVisible = true;
        }

        private void DibujarContorno101()
        // Con propietario anterior al catastro 1967
        {
            GMapOverlay Poligono_parcelas = new GMapOverlay("Poligono");
            List<PointLatLng> puntos = new List<PointLatLng>();
            //puntos.Add(new PointLatLng(42.2150501906204, -8.64652186632156));
            puntos.Add(new PointLatLng(42.2145833568273, - 8.6400631070137));
            puntos.Add(new PointLatLng(42.2145962692978, - 8.64009261131287));
            puntos.Add(new PointLatLng(42.2147244005924, - 8.64023342728615));
            puntos.Add(new PointLatLng(42.2150352917228, - 8.64013954997063));
            puntos.Add(new PointLatLng(42.2152806264547, - 8.64010334014893));
            puntos.Add(new PointLatLng(42.2152607613297, - 8.63958969712257));
            puntos.Add(new PointLatLng(42.215170374932, - 8.63970503211021));
            puntos.Add(new PointLatLng(42.2150233726023, - 8.63948240876198));

            GMapPolygon poligonoPuntos101 = new GMapPolygon(puntos, "101");
            Poligono_parcelas.Polygons.Add(poligonoPuntos101);
            gMapControl1.Overlays.Add(Poligono_parcelas);
            //actualizar mapa
            //gMapControl1.Zoom += 1;
            //gMapControl1.Zoom -= 1;
            poligonoPuntos101.Fill = new SolidBrush(Color.FromArgb(30, Color.Lime));
            poligonoPuntos101.Stroke = new Pen(Color.Lime, 2);
            poligonoPuntos101.IsHitTestVisible = true;
        }


        private void DibujarContorno6007()
            // Con propietario anterior al catastro 1967
        {
            GMapOverlay Poligono_parcelas = new GMapOverlay("Poligono");
            List<PointLatLng> puntos = new List<PointLatLng>();
            //puntos.Add(new PointLatLng(42.2150501906204, -8.64652186632156));
            puntos.Add(new PointLatLng(42.2150263523826, - 8.63947972655296));
            puntos.Add(new PointLatLng(42.215170374932, - 8.63970100879669));
            puntos.Add(new PointLatLng(42.2152597680732, - 8.63958433270454));
            puntos.Add(new PointLatLng(42.2152667208678, - 8.63954275846481));
            puntos.Add(new PointLatLng(42.2151366041568, - 8.6393429338932));


            GMapPolygon poligonoPuntos6007 = new GMapPolygon(puntos, "6007");
            Poligono_parcelas.Polygons.Add(poligonoPuntos6007);
            gMapControl1.Overlays.Add(Poligono_parcelas);
            //actualizar mapa
            //gMapControl1.Zoom += 1;
            //gMapControl1.Zoom -= 1;
            poligonoPuntos6007.Fill = new SolidBrush(Color.FromArgb(30, Color.Lime));
            poligonoPuntos6007.Stroke = new Pen(Color.Lime, 2);
            poligonoPuntos6007.IsHitTestVisible = true;
        }


        private void DibujarContorno338()
        // Con propietario anterior al catastro 1967
        {
            GMapOverlay Poligono_parcelas = new GMapOverlay("Poligono");
            List<PointLatLng> puntos = new List<PointLatLng>();
            //puntos.Add(new PointLatLng(42.2150501906204, -8.64652186632156));
            puntos.Add(new PointLatLng(42.2147293669164, - 8.64023879170418));
            puntos.Add(new PointLatLng(42.2149558308766, - 8.64043191075325));
            puntos.Add(new PointLatLng(42.2152528152779, - 8.64049360156059));
            puntos.Add(new PointLatLng(42.2152965185502, - 8.64044263958931));
            puntos.Add(new PointLatLng(42.2152816197108, - 8.64011004567146));
            puntos.Add(new PointLatLng(42.2150402580224, - 8.64014357328415));


            GMapPolygon poligonoPuntos338 = new GMapPolygon(puntos, "338");
            Poligono_parcelas.Polygons.Add(poligonoPuntos338);
            gMapControl1.Overlays.Add(Poligono_parcelas);
            //actualizar mapa
            //gMapControl1.Zoom += 1;
            //gMapControl1.Zoom -= 1;
            poligonoPuntos338.Fill = new SolidBrush(Color.FromArgb(30, Color.Lime));
            poligonoPuntos338.Stroke = new Pen(Color.Lime, 2);
            poligonoPuntos338.IsHitTestVisible = true;
        }


        private void DibujarContorno14()
        {
            GMapOverlay Poligono_parcelas = new GMapOverlay("Poligono");
            List<PointLatLng> puntos = new List<PointLatLng>();
            //puntos.Add(new PointLatLng(42.2150501906204, -8.64652186632156));
            puntos.Add(new PointLatLng(42.2152781433144, - 8.63955549895763));
            puntos.Add(new PointLatLng(42.2152701972648, - 8.63935634493828));
            puntos.Add(new PointLatLng(42.2152250040889, - 8.63934427499771));
            puntos.Add(new PointLatLng(42.2151723614476, - 8.63939255475998));

            GMapPolygon poligonoPuntos14 = new GMapPolygon(puntos, "14");
            Poligono_parcelas.Polygons.Add(poligonoPuntos14);
            gMapControl1.Overlays.Add(Poligono_parcelas);
            //actualizar mapa
            //gMapControl1.Zoom += 1;
            //gMapControl1.Zoom -= 1;
            poligonoPuntos14.Fill = new SolidBrush(Color.FromArgb(30, Color.Red));
            poligonoPuntos14.Stroke = new Pen(Color.Red, 2);
            poligonoPuntos14.IsHitTestVisible = true;
        }


        private void DibujarContorno15()
        {
            GMapOverlay Poligono_parcelas = new GMapOverlay("Poligono");
            List<PointLatLng> puntos = new List<PointLatLng>();
            //puntos.Add(new PointLatLng(42.2150501906204, -8.64652186632156));
            puntos.Add(new PointLatLng(42.2151385906735, - 8.63934226334095));
            puntos.Add(new PointLatLng(42.2151505097723, - 8.6393603682518));
            puntos.Add(new PointLatLng(42.2152468557384, - 8.63927587866783));
            puntos.Add(new PointLatLng(42.2152697006367, - 8.63928124308586));
            puntos.Add(new PointLatLng(42.2152577815604, - 8.63923564553261));
            puntos.Add(new PointLatLng(42.2152145748898, - 8.63922894001007));
            puntos.Add(new PointLatLng(42.2151475299978, - 8.63930806517601));

            GMapPolygon poligonoPuntos15 = new GMapPolygon(puntos, "15");
            Poligono_parcelas.Polygons.Add(poligonoPuntos15);
            gMapControl1.Overlays.Add(Poligono_parcelas);
            //actualizar mapa
            //gMapControl1.Zoom += 1;
            //gMapControl1.Zoom -= 1;
            poligonoPuntos15.Fill = new SolidBrush(Color.FromArgb(30, Color.Red));
            poligonoPuntos15.Stroke = new Pen(Color.Red, 2);
            poligonoPuntos15.IsHitTestVisible = true;
        }


        private void DibujarContorno104()
        {
            GMapOverlay Poligono_parcelas = new GMapOverlay("Poligono");
            List<PointLatLng> puntos = new List<PointLatLng>();
            //puntos.Add(new PointLatLng(42.2150501906204, -8.64652186632156));
            puntos.Add(new PointLatLng(42.2151455434814, - 8.64083960652351));
            puntos.Add(new PointLatLng(42.2151286580894, - 8.64079669117928));
            puntos.Add(new PointLatLng(42.2150164197809, - 8.64091604948044));
            puntos.Add(new PointLatLng(42.2149339791264, - 8.64078730344772));
            puntos.Add(new PointLatLng(42.2147969088843, - 8.64102736115456));
            puntos.Add(new PointLatLng(42.2148803429801, - 8.64154636859894));
            puntos.Add(new PointLatLng(42.2150660827739, - 8.64130899310112));
            puntos.Add(new PointLatLng(42.2150084736984, - 8.64096701145172));

            GMapPolygon poligonoPuntos104 = new GMapPolygon(puntos, "104");
            Poligono_parcelas.Polygons.Add(poligonoPuntos104);
            gMapControl1.Overlays.Add(Poligono_parcelas);
            //actualizar mapa
            //gMapControl1.Zoom += 1;
            //gMapControl1.Zoom -= 1;
            poligonoPuntos104.Fill = new SolidBrush(Color.FromArgb(30, Color.Red));
            poligonoPuntos104.Stroke = new Pen(Color.Red, 2);
            poligonoPuntos104.IsHitTestVisible = true;
        }


        private void DibujarContorno350()
        {
            GMapOverlay Poligono_parcelas = new GMapOverlay("Poligono");
            List<PointLatLng> puntos = new List<PointLatLng>();
            //puntos.Add(new PointLatLng(42.2150501906204, -8.64652186632156));
            puntos.Add(new PointLatLng(42.2122690021733, - 8.64474356174469));
            puntos.Add(new PointLatLng(42.2120584214892, - 8.64477440714836));
            puntos.Add(new PointLatLng(42.2120415352719, - 8.64465638995171));
            puntos.Add(new PointLatLng(42.211945184416, - 8.64466443657875));
            puntos.Add(new PointLatLng(42.211945184416, - 8.64462018013));
            puntos.Add(new PointLatLng(42.2119958431317, - 8.64457994699478));
            puntos.Add(new PointLatLng(42.2121547721724, - 8.64453032612801));
            puntos.Add(new PointLatLng(42.212184571323, - 8.64462956786156));
            puntos.Add(new PointLatLng(42.2122550959234, - 8.64462420344353));

            GMapPolygon poligonoPuntos350 = new GMapPolygon(puntos, "350");
            Poligono_parcelas.Polygons.Add(poligonoPuntos350);
            gMapControl1.Overlays.Add(Poligono_parcelas);
            //actualizar mapa
            //gMapControl1.Zoom += 1;
            //gMapControl1.Zoom -= 1;
            poligonoPuntos350.Fill = new SolidBrush(Color.FromArgb(30, Color.Red));
            poligonoPuntos350.Stroke = new Pen(Color.Red, 2);
            poligonoPuntos350.IsHitTestVisible = true;
        }


        private void DibujarContorno6004()
        {
            GMapOverlay Poligono_parcelas = new GMapOverlay("Poligono");
            List<PointLatLng> puntos = new List<PointLatLng>();
            //puntos.Add(new PointLatLng(42.2150501906204, -8.64652186632156));
            puntos.Add(new PointLatLng(42.2156719681433, -8.64325225353241));
            puntos.Add(new PointLatLng(42.2159103476789, -8.64275872707367));
            puntos.Add(new PointLatLng(42.2159639829504, -8.64258974790573));
            puntos.Add(new PointLatLng(42.2154852368786, -8.6423134803772));
            puntos.Add(new PointLatLng(42.215441533737, -8.64248514175415));
            puntos.Add(new PointLatLng(42.215433587708, -8.64308059215546));

            GMapPolygon poligonoPuntos6004 = new GMapPolygon(puntos, "6004");
            Poligono_parcelas.Polygons.Add(poligonoPuntos6004);
            gMapControl1.Overlays.Add(Poligono_parcelas);
            //actualizar mapa
            //gMapControl1.Zoom += 1;
            //gMapControl1.Zoom -= 1;
            poligonoPuntos6004.Fill = new SolidBrush(Color.FromArgb(30, Color.Red));
            poligonoPuntos6004.Stroke = new Pen(Color.Red, 2);
            poligonoPuntos6004.IsHitTestVisible = true;
        }


        private void DibujarContorno12()
        // Permuta/Comunal en el catastro 1967
        {
            GMapOverlay Poligono_parcelas = new GMapOverlay("Poligono");
            List<PointLatLng> puntos = new List<PointLatLng>();
            //puntos.Add(new PointLatLng(42.2150501906204, -8.64652186632156));
            puntos.Add(new PointLatLng(42.2154673583243, -8.64136397838593));
            puntos.Add(new PointLatLng(42.2159401450576, -8.6409804224968));
            puntos.Add(new PointLatLng(42.2157812255422, -8.64025086164474));
            puntos.Add(new PointLatLng(42.2157315631116, -8.64025622606277));
            puntos.Add(new PointLatLng(42.2153263162185, -8.64057272672653));
            puntos.Add(new PointLatLng(42.2152706938929, - 8.64090800285339));

            GMapPolygon poligonoPuntos12 = new GMapPolygon(puntos, "12");
            Poligono_parcelas.Polygons.Add(poligonoPuntos12);
            gMapControl1.Overlays.Add(Poligono_parcelas);
            //actualizar mapa
            //gMapControl1.Zoom += 1;
            //gMapControl1.Zoom -= 1;
            poligonoPuntos12.Fill = new SolidBrush(Color.FromArgb(30, Color.Blue));
            poligonoPuntos12.Stroke = new Pen(Color.Blue, 2);
            poligonoPuntos12.IsHitTestVisible = true;
        }


        private void DibujarContorno63()
        // Comunal en el catastro 1967
        {
            GMapOverlay Poligono_parcelas = new GMapOverlay("Poligono");
            List<PointLatLng> puntos = new List<PointLatLng>();
            //puntos.Add(new PointLatLng(42.2150501906204, -8.64652186632156));
            puntos.Add(new PointLatLng(42.2165927041253, -8.63974928855896));
            puntos.Add(new PointLatLng(42.2165380761515, -8.6395588517189));
            puntos.Add(new PointLatLng(42.2164457051064, -8.63942340016365));
            puntos.Add(new PointLatLng(42.216311617865, -8.63959237933159));
            puntos.Add(new PointLatLng(42.2164486848196, - 8.63984048366547));

            GMapPolygon poligonoPuntos63 = new GMapPolygon(puntos, "63");
            Poligono_parcelas.Polygons.Add(poligonoPuntos63);
            gMapControl1.Overlays.Add(Poligono_parcelas);
            //actualizar mapa
            //gMapControl1.Zoom += 1;
            //gMapControl1.Zoom -= 1;
            poligonoPuntos63.Fill = new SolidBrush(Color.FromArgb(30, Color.DarkViolet));
            poligonoPuntos63.Stroke = new Pen(Color.DarkViolet, 2);
            poligonoPuntos63.IsHitTestVisible = true;
        }

        private void DibujarContorno62()
        // Comunal en el catastro 1967
        {
            GMapOverlay Poligono_parcelas = new GMapOverlay("Poligono");
            List<PointLatLng> puntos = new List<PointLatLng>();
            //puntos.Add(new PointLatLng(42.2150501906204, -8.64652186632156));
            puntos.Add(new PointLatLng(42.2165956838317, -8.63974660634995));
            puntos.Add(new PointLatLng(42.2171071979971, -8.63941937685013));
            puntos.Add(new PointLatLng(42.2172929312415, -8.63923564553261));
            puntos.Add(new PointLatLng(42.2170863402189, -8.63897010684013));
            puntos.Add(new PointLatLng(42.2166751426146, -8.63904386758804));
            puntos.Add(new PointLatLng(42.2165927041253, -8.63921150565147));
            puntos.Add(new PointLatLng(42.2164476915819, -8.63942071795464));
            puntos.Add(new PointLatLng(42.2165410558604, - 8.63955616950989));

            GMapPolygon poligonoPuntos62 = new GMapPolygon(puntos, "62");
            Poligono_parcelas.Polygons.Add(poligonoPuntos62);
            gMapControl1.Overlays.Add(Poligono_parcelas);
            //actualizar mapa
            //gMapControl1.Zoom += 1;
            //gMapControl1.Zoom -= 1;
            poligonoPuntos62.Fill = new SolidBrush(Color.FromArgb(30, Color.DarkViolet));
            poligonoPuntos62.Stroke = new Pen(Color.DarkViolet, 2);
            poligonoPuntos62.IsHitTestVisible = true;
        }
        
        private void DibujarContorno61()
        // Comunal en el catastro 1967
        {
            GMapOverlay Poligono_parcelas = new GMapOverlay("Poligono");
            List<PointLatLng> puntos = new List<PointLatLng>();
            //puntos.Add(new PointLatLng(42.2150501906204, -8.64652186632156));
            puntos.Add(new PointLatLng(42.2172949176904, -8.63923296332359));
            puntos.Add(new PointLatLng(42.2174697249453, -8.63897278904915));
            puntos.Add(new PointLatLng(42.2174558198405, -8.63892182707787));
            puntos.Add(new PointLatLng(42.2172194325898, -8.63893926143646));
            puntos.Add(new PointLatLng(42.2170932928124, - 8.63897010684013));

            GMapPolygon poligonoPuntos61 = new GMapPolygon(puntos, "61");
            Poligono_parcelas.Polygons.Add(poligonoPuntos61);
            gMapControl1.Overlays.Add(Poligono_parcelas);
            //actualizar mapa
            //gMapControl1.Zoom += 1;
            //gMapControl1.Zoom -= 1;
            poligonoPuntos61.Fill = new SolidBrush(Color.FromArgb(30, Color.DarkViolet));
            poligonoPuntos61.Stroke = new Pen(Color.DarkViolet, 2);
            poligonoPuntos61.IsHitTestVisible = true;
        }


        private void DibujarContorno36()
        // Permuta/Comunal en el catastro 1967
        {
            GMapOverlay Poligono_parcelas = new GMapOverlay("Poligono");
            List<PointLatLng> puntos = new List<PointLatLng>();
            //puntos.Add(new PointLatLng(42.2150501906204, -8.64652186632156));
            puntos.Add(new PointLatLng(42.215449479765, -8.63879442214966));
            puntos.Add(new PointLatLng(42.2160752263313, -8.63791197538376));
            puntos.Add(new PointLatLng(42.2157911580236, -8.63736748695374));
            puntos.Add(new PointLatLng(42.2157315631116, -8.63741844892502));
            puntos.Add(new PointLatLng(42.2156799141424, -8.63735407590866));
            puntos.Add(new PointLatLng(42.2156699816434, -8.63715022802353));
            puntos.Add(new PointLatLng(42.2156521031414, -8.63717705011368));
            puntos.Add(new PointLatLng(42.2156600491429, -8.63738358020782));
            puntos.Add(new PointLatLng(42.2156322381333, -8.63767057657242));
            puntos.Add(new PointLatLng(42.2156183326238, - 8.63803535699844));

            GMapPolygon poligonoPuntos36 = new GMapPolygon(puntos, "36");
            Poligono_parcelas.Polygons.Add(poligonoPuntos36);
            gMapControl1.Overlays.Add(Poligono_parcelas);
            //actualizar mapa
            //gMapControl1.Zoom += 1;
            //gMapControl1.Zoom -= 1;
            poligonoPuntos36.Fill = new SolidBrush(Color.FromArgb(30, Color.Blue));
            poligonoPuntos36.Stroke = new Pen(Color.Blue, 2);
            poligonoPuntos36.IsHitTestVisible = true;
        }


        private void DibujarContorno292()
        {
            GMapOverlay Poligono_parcelas = new GMapOverlay("Poligono");
            List<PointLatLng> puntos = new List<PointLatLng>();
            //puntos.Add(new PointLatLng(42.2150501906204, -8.64652186632156));
            puntos.Add(new PointLatLng(42.2154852368786, -8.63830626010895));
            puntos.Add(new PointLatLng(42.2155090749432, -8.63827407360077));
            puntos.Add(new PointLatLng(42.2155130479531, -8.63812655210495));
            puntos.Add(new PointLatLng(42.21548920989, -8.63797500729561));
            puntos.Add(new PointLatLng(42.215383925004, -8.63806620240211));
            puntos.Add(new PointLatLng(42.2154683515775, - 8.63828614354134));


            GMapPolygon poligonoPuntos292 = new GMapPolygon(puntos, "292");
            Poligono_parcelas.Polygons.Add(poligonoPuntos292);
            gMapControl1.Overlays.Add(Poligono_parcelas);
            //actualizar mapa
            //gMapControl1.Zoom += 1;
            //gMapControl1.Zoom -= 1;
            poligonoPuntos292.Fill = new SolidBrush(Color.FromArgb(30, Color.Red));
            poligonoPuntos292.Stroke = new Pen(Color.Red, 2);
            poligonoPuntos292.IsHitTestVisible = true;
        }


        private void DibujarContorno35()
        {
            GMapOverlay Poligono_parcelas = new GMapOverlay("Poligono");
            List<PointLatLng> puntos = new List<PointLatLng>();
            //puntos.Add(new PointLatLng(42.2150501906204, -8.64652186632156));
            puntos.Add(new PointLatLng(42.2154882166372, -8.63830894231796));
            puntos.Add(new PointLatLng(42.2155031154279, -8.63833107054234));
            puntos.Add(new PointLatLng(42.2155661869362, -8.63804809749126));
            puntos.Add(new PointLatLng(42.2155835688369, -8.63786973059177));
            puntos.Add(new PointLatLng(42.2154911963956, -8.6379736661911));
            puntos.Add(new PointLatLng(42.2155150344579, -8.63812655210495));
            puntos.Add(new PointLatLng(42.2155110614481, - 8.63827608525753));


            GMapPolygon poligonoPuntos35 = new GMapPolygon(puntos, "35");
            Poligono_parcelas.Polygons.Add(poligonoPuntos35);
            gMapControl1.Overlays.Add(Poligono_parcelas);
            //actualizar mapa
            //gMapControl1.Zoom += 1;
            //gMapControl1.Zoom -= 1;
            poligonoPuntos35.Fill = new SolidBrush(Color.FromArgb(30, Color.Red));
            poligonoPuntos35.Stroke = new Pen(Color.Red, 2);
            poligonoPuntos35.IsHitTestVisible = true;
        }


        private void DibujarContorno289()
        {
            GMapOverlay Poligono_parcelas = new GMapOverlay("Poligono");
            List<PointLatLng> puntos = new List<PointLatLng>();
            //puntos.Add(new PointLatLng(42.2150501906204, -8.64652186632156));
            puntos.Add(new PointLatLng(42.2155746295743, -8.63780066370964));
            puntos.Add(new PointLatLng(42.2153610801471, -8.63802194595337));
            puntos.Add(new PointLatLng(42.2152140782613, -8.63777250051498));
            puntos.Add(new PointLatLng(42.2150968738099, -8.63794282078743));
            puntos.Add(new PointLatLng(42.2148555114155, -8.63754317164421));
            puntos.Add(new PointLatLng(42.2150293321629, -8.63739296793938));
            puntos.Add(new PointLatLng(42.2152329501441, -8.63763973116875));
            puntos.Add(new PointLatLng(42.2153829317495, -8.63738358020782));
            puntos.Add(new PointLatLng(42.2155348995029, -8.63728299736977));
            puntos.Add(new PointLatLng(42.2155766160772, -8.63724544644356));
            puntos.Add(new PointLatLng(42.2156074068643, -8.63730981945992));

            GMapPolygon poligonoPuntos289 = new GMapPolygon(puntos, "289");
            Poligono_parcelas.Polygons.Add(poligonoPuntos289);
            gMapControl1.Overlays.Add(Poligono_parcelas);
            //actualizar mapa
            //gMapControl1.Zoom += 1;
            //gMapControl1.Zoom -= 1;
            poligonoPuntos289.Fill = new SolidBrush(Color.FromArgb(30, Color.Red));
            poligonoPuntos289.Stroke = new Pen(Color.Red, 2);
            poligonoPuntos289.IsHitTestVisible = true;
        }


        private void DibujarContorno290()
        {
            GMapOverlay Poligono_parcelas = new GMapOverlay("Poligono");
            List<PointLatLng> puntos = new List<PointLatLng>();
            //puntos.Add(new PointLatLng(42.2150501906204, -8.64652186632156));
            puntos.Add(new PointLatLng(42.215379951986, -8.63738358020782));
            puntos.Add(new PointLatLng(42.2152190445468, -8.63739296793938));
            puntos.Add(new PointLatLng(42.2150342984629, -8.63739162683487));
            puntos.Add(new PointLatLng(42.2152329501441, - 8.63763302564621));

            GMapPolygon poligonoPuntos290 = new GMapPolygon(puntos, "290");
            Poligono_parcelas.Polygons.Add(poligonoPuntos290);
            gMapControl1.Overlays.Add(Poligono_parcelas);
            //actualizar mapa
            //gMapControl1.Zoom += 1;
            //gMapControl1.Zoom -= 1;
            poligonoPuntos290.Fill = new SolidBrush(Color.FromArgb(30, Color.Red));
            poligonoPuntos290.Stroke = new Pen(Color.Red, 2);
            poligonoPuntos290.IsHitTestVisible = true;
        }


        private void DibujarContorno32217()
        {
            GMapOverlay Poligono_parcelas = new GMapOverlay("Poligono");
            List<PointLatLng> puntos = new List<PointLatLng>();
            //puntos.Add(new PointLatLng(42.2150501906204, -8.64652186632156));
            puntos.Add(new PointLatLng(42.215297511806, -8.63732323050499));
            puntos.Add(new PointLatLng(42.2150750221085, -8.63729640841484));
            puntos.Add(new PointLatLng(42.2150551569188, -8.63726288080215));
            puntos.Add(new PointLatLng(42.2151316378648, -8.63702550530434));
            puntos.Add(new PointLatLng(42.2151564693208, -8.63696649670601));

            GMapPolygon poligonoPuntos32217 = new GMapPolygon(puntos, "32217");
            Poligono_parcelas.Polygons.Add(poligonoPuntos32217);
            gMapControl1.Overlays.Add(Poligono_parcelas);
            //actualizar mapa
            //gMapControl1.Zoom += 1;
            //gMapControl1.Zoom -= 1;
            poligonoPuntos32217.Fill = new SolidBrush(Color.FromArgb(30, Color.Red));
            poligonoPuntos32217.Stroke = new Pen(Color.Red, 2);
            poligonoPuntos32217.IsHitTestVisible = true;
        }


        private void DibujarContorno178()
        {
            GMapOverlay Poligono_parcelas = new GMapOverlay("Poligono");
            List<PointLatLng> puntos = new List<PointLatLng>();
            //puntos.Add(new PointLatLng(42.2150501906204, -8.64652186632156));
            puntos.Add(new PointLatLng(42.2151445502231, -8.63693431019783));
            puntos.Add(new PointLatLng(42.2150879344784, -8.6370550096035));
            puntos.Add(new PointLatLng(42.2150273456427, -8.6372534930706));
            puntos.Add(new PointLatLng(42.2149766893584, -8.63724946975708));
            puntos.Add(new PointLatLng(42.2148863025542, -8.6373071372509));
            puntos.Add(new PointLatLng(42.214842598998, -8.63730043172836));
            puntos.Add(new PointLatLng(42.2148167741552, -8.6372521519661));
            puntos.Add(new PointLatLng(42.2148763699304, -8.6366768181324));
            puntos.Add(new PointLatLng(42.214875376668, -8.63664999604225));
            puntos.Add(new PointLatLng(42.2148932553899, -8.63662719726563));
            puntos.Add(new PointLatLng(42.2148902756032, -8.63652393221855));
            puntos.Add(new PointLatLng(42.2149856287057, -8.63639518618584));
            puntos.Add(new PointLatLng(42.2150154265207, -8.6363884806633));
            puntos.Add(new PointLatLng(42.2150164197809, -8.63641932606697));
            puntos.Add(new PointLatLng(42.2149905950092, - 8.63654404878616));

            GMapPolygon poligonoPuntos178 = new GMapPolygon(puntos, "178");
            Poligono_parcelas.Polygons.Add(poligonoPuntos178);
            gMapControl1.Overlays.Add(Poligono_parcelas);
            //actualizar mapa
            //gMapControl1.Zoom += 1;
            //gMapControl1.Zoom -= 1;
            poligonoPuntos178.Fill = new SolidBrush(Color.FromArgb(30, Color.Red));
            poligonoPuntos178.Stroke = new Pen(Color.Red, 2);
            poligonoPuntos178.IsHitTestVisible = true;
        }


        private void DibujarContorno1038()
        {
            GMapOverlay Poligono_parcelas = new GMapOverlay("Poligono");
            List<PointLatLng> puntos = new List<PointLatLng>();
            //puntos.Add(new PointLatLng(42.2150501906204, -8.64652186632156));
            puntos.Add(new PointLatLng(42.2149627837047, -8.63734737038612));
            puntos.Add(new PointLatLng(42.2149449050025, -8.6373433470726));
            puntos.Add(new PointLatLng(42.2147879695103, -8.63743722438812));
            puntos.Add(new PointLatLng(42.2146588450788, -8.63757267594337));
            puntos.Add(new PointLatLng(42.2146697710025, - 8.63761022686958));

            GMapPolygon poligonoPuntos1038 = new GMapPolygon(puntos, "1038");
            Poligono_parcelas.Polygons.Add(poligonoPuntos1038);
            gMapControl1.Overlays.Add(Poligono_parcelas);
            //actualizar mapa
            gMapControl1.Zoom += 1;
            gMapControl1.Zoom -= 1;
            poligonoPuntos1038.Fill = new SolidBrush(Color.FromArgb(30, Color.Red));
            poligonoPuntos1038.Stroke = new Pen(Color.Red, 2);
            poligonoPuntos1038.IsHitTestVisible = true;
        }


        private void DibujarContorno16()
        {
            GMapOverlay Poligono_parcelas = new GMapOverlay("Poligono");
            List<PointLatLng> puntos = new List<PointLatLng>();
            //puntos.Add(new PointLatLng(42.2150501906204, -8.64652186632156));
            puntos.Add(new PointLatLng(42.214192008393, -8.64061027765274));
            puntos.Add(new PointLatLng(42.2141741294726, -8.64049762487412));
            puntos.Add(new PointLatLng(42.214275443288, -8.6401355266571));
            puntos.Add(new PointLatLng(42.2143588780727, -8.64000141620636));
            puntos.Add(new PointLatLng(42.2145575318785, -8.63979488611221));
            puntos.Add(new PointLatLng(42.2146012356319, -8.63968223333359));
            puntos.Add(new PointLatLng(42.2146091817656, -8.6395400762558));
            puntos.Add(new PointLatLng(42.2146608316105, -8.63954812288284));
            puntos.Add(new PointLatLng(42.214750225473, -8.63942474126816));
            puntos.Add(new PointLatLng(42.2148177674186, -8.63943815231323));
            puntos.Add(new PointLatLng(42.2148793497177, -8.63951057195663));
            puntos.Add(new PointLatLng(42.2144681377432, -8.64004164934158));
            puntos.Add(new PointLatLng(42.2143449722829, -8.64026963710785));

            GMapPolygon poligonoPuntos16 = new GMapPolygon(puntos, "16");
            Poligono_parcelas.Polygons.Add(poligonoPuntos16);
            gMapControl1.Overlays.Add(Poligono_parcelas);
            //actualizar mapa
            gMapControl1.Zoom += 1;
            gMapControl1.Zoom -= 1;
            poligonoPuntos16.Fill = new SolidBrush(Color.FromArgb(30, Color.Red));
            poligonoPuntos16.Stroke = new Pen(Color.Red, 2);
            poligonoPuntos16.IsHitTestVisible = true;
        }

        private void DibujarContorno321()
        // Con propietario anterior al catastro 1967
        {
            GMapOverlay Poligono_parcelas = new GMapOverlay("Poligono");
            List<PointLatLng> puntos = new List<PointLatLng>();
            //puntos.Add(new PointLatLng(42.2150501906204, -8.64652186632156));
            puntos.Add(new PointLatLng(42.2152051389464, -8.641422316432));
            puntos.Add(new PointLatLng(42.2152066288323, -8.64166036248207));
            puntos.Add(new PointLatLng(42.2151932198579, -8.64166840910912));
            puntos.Add(new PointLatLng(42.2152051389464, -8.64175893366337));
            puntos.Add(new PointLatLng(42.2151803075095, -8.64177636802197));
            puntos.Add(new PointLatLng(42.2151654086426, -8.6417642980814));
            puntos.Add(new PointLatLng(42.215149019885, -8.64172607660294));
            puntos.Add(new PointLatLng(42.2151336243816, -8.64164225757122));
            puntos.Add(new PointLatLng(42.2151619322399, -8.6414410918951));
            puntos.Add(new PointLatLng(42.2152051389464, -8.641422316432));

            GMapPolygon poligonoPuntos321 = new GMapPolygon(puntos, "321");
            Poligono_parcelas.Polygons.Add(poligonoPuntos321);
            gMapControl1.Overlays.Add(Poligono_parcelas);
            //actualizar mapa
            gMapControl1.Zoom += 1;
            gMapControl1.Zoom -= 1;
            poligonoPuntos321.Fill = new SolidBrush(Color.FromArgb(30, Color.Lime));
            poligonoPuntos321.Stroke = new Pen(Color.Lime, 2);
            poligonoPuntos321.IsHitTestVisible = true;
        }


        private void DibujarContorno103()
        // Con propietario anterior al catastro 1967
        {
            GMapOverlay Poligono_parcelas = new GMapOverlay("Poligono");
            List<PointLatLng> puntos = new List<PointLatLng>();
            puntos.Add(new PointLatLng(42.2148654440425, -8.64068269729614));
            puntos.Add(new PointLatLng(42.2149319926033, -8.64078462123871));
            puntos.Add(new PointLatLng(42.2145853433614, -8.64138945937157));
            puntos.Add(new PointLatLng(42.2146052086989, -8.64123791456223));
            puntos.Add(new PointLatLng(42.2146747373308, -8.64106088876724));

            GMapPolygon poligonoPuntos103 = new GMapPolygon(puntos, "103");
            Poligono_parcelas.Polygons.Add(poligonoPuntos103);
            gMapControl1.Overlays.Add(Poligono_parcelas);
            //actualizar mapa
            gMapControl1.Zoom += 1;
            gMapControl1.Zoom -= 1;
            poligonoPuntos103.Fill = new SolidBrush(Color.FromArgb(30, Color.Lime));
            poligonoPuntos103.Stroke = new Pen(Color.Lime, 2);
            poligonoPuntos103.IsHitTestVisible = true;
        }
        private void DibujarContorno360()
        // Con propietario anterior al catastro 1967
        {
            GMapOverlay Poligono_parcelas = new GMapOverlay("Poligono");
            List<PointLatLng> puntos = new List<PointLatLng>();
            puntos.Add(new PointLatLng(42.2147144679432, -8.64088654518127));
            puntos.Add(new PointLatLng(42.2145714376215, -8.64116549491882));
            puntos.Add(new PointLatLng(42.2144085415827, -8.64095360040665));
            puntos.Add(new PointLatLng(42.2145654780177, -8.6406397819519));
            puntos.Add(new PointLatLng(42.2146369932258, -8.64078730344772));

            GMapPolygon poligonoPuntos360 = new GMapPolygon(puntos, "360");
            Poligono_parcelas.Polygons.Add(poligonoPuntos360);
            gMapControl1.Overlays.Add(Poligono_parcelas);
            //actualizar mapa
            gMapControl1.Zoom += 1;
            gMapControl1.Zoom -= 1;
            poligonoPuntos360.Fill = new SolidBrush(Color.FromArgb(30, Color.Lime));
            poligonoPuntos360.Stroke = new Pen(Color.Lime, 2);
            poligonoPuntos360.IsHitTestVisible = true;
        }

        private void DibujarContorno100()
        {
            GMapOverlay Poligono_parcelas = new GMapOverlay("Poligono");
            List<PointLatLng> puntos = new List<PointLatLng>();
            puntos.Add(new PointLatLng(42.2150134400001, -8.64052042365074));
            puntos.Add(new PointLatLng(42.2151276648309, -8.6407946795225));
            puntos.Add(new PointLatLng(42.215016916411, -8.64091202616692));
            puntos.Add(new PointLatLng(42.2148669339364, -8.64068068563938));
            puntos.Add(new PointLatLng(42.2149339791264, -8.64055529236794));
            puntos.Add(new PointLatLng(42.2149886084878, -8.64051572978497));

            GMapPolygon poligonoPuntos100 = new GMapPolygon(puntos, "100");
            Poligono_parcelas.Polygons.Add(poligonoPuntos100);
            gMapControl1.Overlays.Add(Poligono_parcelas);
            //actualizar mapa
            gMapControl1.Zoom += 1;
            gMapControl1.Zoom -= 1;
            poligonoPuntos100.Fill = new SolidBrush(Color.FromArgb(30, Color.Red));
            poligonoPuntos100.Stroke = new Pen(Color.Red, 2);
            poligonoPuntos100.IsHitTestVisible = true;
        }

        private void DibujarContorno105()
        {
            GMapOverlay Poligono_parcelas = new GMapOverlay("Poligono");
            List<PointLatLng> puntos = new List<PointLatLng>();
            puntos.Add(new PointLatLng(42.2146017322653, -8.64136531949043));
            puntos.Add(new PointLatLng(42.214794425725, -8.64102937281132));
            puntos.Add(new PointLatLng(42.2148783564553, -8.6415483802557));
            puntos.Add(new PointLatLng(42.2147090049855, -8.64176228642464));
            puntos.Add(new PointLatLng(42.2146007389985, -8.6414135992527));

            GMapPolygon poligonoPuntos105 = new GMapPolygon(puntos, "105");
            Poligono_parcelas.Polygons.Add(poligonoPuntos105);
            gMapControl1.Overlays.Add(Poligono_parcelas);
            //actualizar mapa
            gMapControl1.Zoom += 1;
            gMapControl1.Zoom -= 1;
            poligonoPuntos105.Fill = new SolidBrush(Color.FromArgb(30, Color.Red));
            poligonoPuntos105.Stroke = new Pen(Color.Red, 2);
            poligonoPuntos105.IsHitTestVisible = true;
        }

        private void DibujarContorno106()
        {
            GMapOverlay Poligono_parcelas = new GMapOverlay("Poligono");
            List<PointLatLng> puntos = new List<PointLatLng>();
            puntos.Add(new PointLatLng(42.2147085083529, -8.64177167415619));
            puntos.Add(new PointLatLng(42.2150680692928, -8.64131435751915));
            puntos.Add(new PointLatLng(42.2150919075148, -8.64148870110512));
            puntos.Add(new PointLatLng(42.2150670760333, -8.64158794283867));
            puntos.Add(new PointLatLng(42.2148048549934, -8.64199697971344));

            GMapPolygon poligonoPuntos106 = new GMapPolygon(puntos, "106");
            Poligono_parcelas.Polygons.Add(poligonoPuntos106);
            gMapControl1.Overlays.Add(Poligono_parcelas);
            //actualizar mapa
            gMapControl1.Zoom += 1;
            gMapControl1.Zoom -= 1;
            poligonoPuntos106.Fill = new SolidBrush(Color.FromArgb(30, Color.Red));
            poligonoPuntos106.Stroke = new Pen(Color.Red, 2);
            poligonoPuntos106.IsHitTestVisible = true;
        }
        private void DibujarContorno107()
        {
            GMapOverlay Poligono_parcelas = new GMapOverlay("Poligono");
            List<PointLatLng> puntos = new List<PointLatLng>();
            puntos.Add(new PointLatLng(42.2148063448887, -8.64200033247471));
            puntos.Add(new PointLatLng(42.2150680692928, -8.64159263670444));
            puntos.Add(new PointLatLng(42.2150889277375, -8.64172205328941));
            puntos.Add(new PointLatLng(42.2148624642546, -8.64207744598389));

            GMapPolygon poligonoPuntos107 = new GMapPolygon(puntos, "107");
            Poligono_parcelas.Polygons.Add(poligonoPuntos107);
            gMapControl1.Overlays.Add(Poligono_parcelas);
            //actualizar mapa
            gMapControl1.Zoom += 1;
            gMapControl1.Zoom -= 1;
            poligonoPuntos107.Fill = new SolidBrush(Color.FromArgb(30, Color.Red));
            poligonoPuntos107.Stroke = new Pen(Color.Red, 2);
            poligonoPuntos107.IsHitTestVisible = true;
        }
        private void DibujarContorno108()
        {
            GMapOverlay Poligono_parcelas = new GMapOverlay("Poligono");
            List<PointLatLng> puntos = new List<PointLatLng>();
            puntos.Add(new PointLatLng(42.2148639541486, -8.64208079874516));
            puntos.Add(new PointLatLng(42.2150889277375, -8.64172607660294));
            puntos.Add(new PointLatLng(42.2151058131401, -8.64181526005268));
            puntos.Add(new PointLatLng(42.2150998535868, -8.64190310239792));
            puntos.Add(new PointLatLng(42.2149399386954, -8.64215657114983));

            GMapPolygon poligonoPuntos108 = new GMapPolygon(puntos, "108");
            Poligono_parcelas.Polygons.Add(poligonoPuntos108);
            gMapControl1.Overlays.Add(Poligono_parcelas);
            //actualizar mapa
            gMapControl1.Zoom += 1;
            gMapControl1.Zoom -= 1;
            poligonoPuntos108.Fill = new SolidBrush(Color.FromArgb(30, Color.Red));
            poligonoPuntos108.Stroke = new Pen(Color.Red, 2);
            poligonoPuntos108.IsHitTestVisible = true;
        }
        private void DibujarContorno109()
        {
            GMapOverlay Poligono_parcelas = new GMapOverlay("Poligono");
            List<PointLatLng> puntos = new List<PointLatLng>();
            puntos.Add(new PointLatLng(42.2149419252183, -8.64215791225433));
            puntos.Add(new PointLatLng(42.215103329993, -8.64190444350243));
            puntos.Add(new PointLatLng(42.2152190445468, -8.6419366300106));
            puntos.Add(new PointLatLng(42.2151316378648, -8.64210024476051));
            puntos.Add(new PointLatLng(42.2150362849828, -8.64223301410675));

            GMapPolygon poligonoPuntos109 = new GMapPolygon(puntos, "109");
            Poligono_parcelas.Polygons.Add(poligonoPuntos109);
            gMapControl1.Overlays.Add(Poligono_parcelas);
            //actualizar mapa
            gMapControl1.Zoom += 1;
            gMapControl1.Zoom -= 1;
            poligonoPuntos109.Fill = new SolidBrush(Color.FromArgb(30, Color.Red));
            poligonoPuntos109.Stroke = new Pen(Color.Red, 2);
            poligonoPuntos109.IsHitTestVisible = true;
        }
        private void DibujarContorno342()
        {
            GMapOverlay Poligono_parcelas = new GMapOverlay("Poligono");
            List<PointLatLng> puntos = new List<PointLatLng>();
            puntos.Add(new PointLatLng(42.2151778243652, -8.64273257553577));
            puntos.Add(new PointLatLng(42.2150973704394, -8.64283047616482));
            puntos.Add(new PointLatLng(42.2148162775235, -8.64262528717518));
            puntos.Add(new PointLatLng(42.2148391225775, -8.64257834851742));
            puntos.Add(new PointLatLng(42.2149180869397, -8.64244356751442));

            GMapPolygon poligonoPuntos342 = new GMapPolygon(puntos, "342");
            Poligono_parcelas.Polygons.Add(poligonoPuntos342);
            gMapControl1.Overlays.Add(Poligono_parcelas);
            //actualizar mapa
            gMapControl1.Zoom += 1;
            gMapControl1.Zoom -= 1;
            poligonoPuntos342.Fill = new SolidBrush(Color.FromArgb(30, Color.Red));
            poligonoPuntos342.Stroke = new Pen(Color.Red, 2);
            poligonoPuntos342.IsHitTestVisible = true;
        }
        private void DibujarContorno337()
        {
            GMapOverlay Poligono_parcelas = new GMapOverlay("Poligono");
            List<PointLatLng> puntos = new List<PointLatLng>();
            puntos.Add(new PointLatLng(42.2147795267671, -8.6419453471899));
            puntos.Add(new PointLatLng(42.2146161346318, -8.64222697913647));
            puntos.Add(new PointLatLng(42.2146722541667, -8.6423597484827));
            puntos.Add(new PointLatLng(42.2147442658861, -8.6422511190176));
            puntos.Add(new PointLatLng(42.2149170936779, -8.64243485033512));
            puntos.Add(new PointLatLng(42.2150233726023, -8.64227458834648));
            puntos.Add(new PointLatLng(42.2150347950929, -8.64223569631577));
            puntos.Add(new PointLatLng(42.2149399386954, -8.64216059446335));
            puntos.Add(new PointLatLng(42.2148604777292, -8.64208281040192));
            puntos.Add(new PointLatLng(42.2148028684662, -8.64200100302696));

            GMapPolygon poligonoPuntos337 = new GMapPolygon(puntos, "337");
            Poligono_parcelas.Polygons.Add(poligonoPuntos337);
            gMapControl1.Overlays.Add(Poligono_parcelas);
            //actualizar mapa
            gMapControl1.Zoom += 1;
            gMapControl1.Zoom -= 1;
            poligonoPuntos337.Fill = new SolidBrush(Color.FromArgb(30, Color.Red));
            poligonoPuntos337.Stroke = new Pen(Color.Red, 2);
            poligonoPuntos337.IsHitTestVisible = true;
        }
        private void DibujarContorno339()
        {
            GMapOverlay Poligono_parcelas = new GMapOverlay("Poligono");
            List<PointLatLng> puntos = new List<PointLatLng>();
            puntos.Add(new PointLatLng(42.2145441227662, -8.64143170416355));
            puntos.Add(new PointLatLng(42.2143832131975, -8.64163622260094));
            puntos.Add(new PointLatLng(42.2142530946662, -8.64146187901497));
            puntos.Add(new PointLatLng(42.2144323800537, -8.64120706915855));
            puntos.Add(new PointLatLng(42.214567961186, -8.64117085933685));

            GMapPolygon poligonoPuntos339 = new GMapPolygon(puntos, "339");
            Poligono_parcelas.Polygons.Add(poligonoPuntos339);
            gMapControl1.Overlays.Add(Poligono_parcelas);
            //actualizar mapa
            gMapControl1.Zoom += 1;
            gMapControl1.Zoom -= 1;
            poligonoPuntos339.Fill = new SolidBrush(Color.FromArgb(30, Color.Red));
            poligonoPuntos339.Stroke = new Pen(Color.Red, 2);
            poligonoPuntos339.IsHitTestVisible = true;
        }






        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            Crear_Datos frm = new Crear_Datos();
            frm.ShowDialog();
        }

 
    }
}

