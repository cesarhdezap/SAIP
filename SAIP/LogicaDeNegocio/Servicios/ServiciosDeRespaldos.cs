using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoADatos;
using LogicaDeNegocio.Interfaces;

namespace LogicaDeNegocio.Servicios
{
    public class ServiciosDeRespaldos
    {
        private readonly string RUTA_RESPALDOS = "Respaldos/";
        private IActualizarBarraDeProgreso ActualizarBarraDeProgreso;

        public ServiciosDeRespaldos(IActualizarBarraDeProgreso actualizarBarraDeProgreso)
        {
            ActualizarBarraDeProgreso = actualizarBarraDeProgreso;
        }

        private void ValidarDirectorio()
        {
            if (!Directory.Exists(@RUTA_RESPALDOS))
            {
                Directory.CreateDirectory(RUTA_RESPALDOS);
            }
        }
        public string ObtenerRutaPorDefectoCompleta()
        {
            ValidarDirectorio();
            return Path.GetFullPath(RUTA_RESPALDOS);
        }

        public void GenerarRespaldo(string nombreUsuario, string ruta)
        {

            if (string.IsNullOrEmpty(ruta))
            {
                throw new InvalidOperationException("ruta es null");
            }
            if (!ruta.EndsWith("\\"))
            {
                ruta += "\\";
            }

            ValidarDirectorio();
            string backupname = DateTime.Now.ToString("MM-dd-yyyy_HH-mm") + "_" + nombreUsuario + ".bak";

            ruta += backupname;

            string connection;
            string databaseName;
            using (ModeloDeDatosContainer context = new ModeloDeDatosContainer())
            {
                connection = context.Database.Connection.ConnectionString;
                databaseName = context.Database.Connection.Database;
            }


            if (!string.IsNullOrEmpty(connection) && !string.IsNullOrEmpty(databaseName))
            {
                SqlConnection con = new SqlConnection(connection);
                con.FireInfoMessageEventOnUserErrors = true;
                con.InfoMessage += OnInfoMessage;
                con.Open();

                using (var cmd = new SqlCommand(string.Format(
                    @"BACKUP DATABASE {0} TO DISK = N{1} WITH STATS = 10",
                    QuoteIdentifier(databaseName),
                    QuoteString("/tmp/" + backupname)), con))
                {
                    cmd.ExecuteNonQuery();
                }
                con.Close();
                con.InfoMessage -= OnInfoMessage;
                con.FireInfoMessageEventOnUserErrors = false;
            }

            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/C docker cp sql1:/tmp/" + backupname + " " + ruta.Replace("\\", @"\");
            process.StartInfo = startInfo;
            process.Start();

        }

        private void OnInfoMessage(object sender, SqlInfoMessageEventArgs e)
        {
            foreach (SqlError info in e.Errors)
            {
                if (info.Class > 10)
                {
                    throw new Exception("Hubo un error al generar el respaldo.");
                }
                else
                {
                    ActualizarBarraDeProgreso.ActualizarBarraDeProgreso(10);
                }
            }
        }

        private string QuoteIdentifier(string name)
        {
            return "[" + name.Replace("]", "]]") + "]";
        }

        private string QuoteString(string text)
        {
            return "'" + text.Replace("'", "''") + "'";
        }
    }
}
