using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Web.Services;
using wsArqApp.Models;
using wsArqApp.Models.Respuestas;

namespace wsArqApp
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class Bd : WebService
    {
        public Bd()
        {

        }

        #region Conexion

        private MySqlConnection SqlConnection { get; set; }

        private void CrearConexionSql()
        {
            // string connString = "server=198.71.225.51;port=3306;userid=rentec;password=r3nt3n2019;database=rentec;";
            string connString = "server=198.71.225.51;port=3306;userid=dev;password=r3nt3n2019;database=bitacoraApp_dev;";
            SqlConnection = new MySqlConnection(connString);
        }

        private void AbrirConexion()
        {
            if (SqlConnection != null)
                SqlConnection.Open();
        }

        private void CerrarConexion()
        {
            if (SqlConnection != null)
                SqlConnection.Close();
        }

        #endregion

        #region Empleado

        [WebMethod]
        public List<Empleado> ConsultarEmpleados()
        {
            try
            {
                CrearConexionSql();
                AbrirConexion();
                string query = "SELECT IdEmpleado" +
                                "       ,Nombre" +
                                "       ,Apellido" +
                                "       ,Categoria" +
                                "       ,Imss" +
                                "       ,SueldoSemanal" +
                                "       ,Baja" +
                                "       ,MotivoBaja" +
                                "       ,FechaIngreso " +
                                "FROM Empleado ";
                MySqlCommand cmd = new MySqlCommand(query, SqlConnection);
                MySqlDataReader reader = cmd.ExecuteReader();
                List<Empleado> listaEmpleados = new List<Empleado>();

                while (reader.Read())
                {
                    var empleado = new Empleado
                    {
                        IdEmpleado = reader.GetInt32(0),
                        Nombre = reader.GetString(1),
                        Apellido = reader.GetString(2),
                        Categoria = reader.GetString(3),
                        Imss = reader.GetString(4),
                        SueldoSemanal = reader.GetDecimal(5),
                        Baja = reader.GetBoolean(6),
                        MotivoBaja = reader.GetString(7),
                        FechaIngreso = reader.GetString(8)
                    };

                    listaEmpleados.Add(empleado);
                }

                CerrarConexion();
                return listaEmpleados;
            }
            catch (Exception ex)
            {
                CerrarConexion();
                throw ex;
            }
        }

        [WebMethod]
        public RespuestaConsultarEmpleados ConsultarListaEmpleados()
        {
            var respuesta = new RespuestaConsultarEmpleados();
            try
            {
                CrearConexionSql();
                AbrirConexion();
                string query = "SELECT IdEmpleado" +
                                "       ,Nombre" +
                                "       ,Apellido" +
                                "       ,Categoria" +
                                "       ,Imss" +
                                "       ,SueldoSemanal" +
                                "       ,Baja" +
                                "       ,MotivoBaja" +
                                "       ,FechaIngreso " +
                                "FROM Empleado ";
                MySqlCommand cmd = new MySqlCommand(query, SqlConnection);
                MySqlDataReader reader = cmd.ExecuteReader();
                List<Empleado> listaEmpleados = new List<Empleado>();

                while (reader.Read())
                {
                    var empleado = new Empleado
                    {
                        IdEmpleado = reader.GetInt32(0),
                        Nombre = reader.GetString(1),
                        Apellido = reader.GetString(2),
                        Categoria = reader.GetString(3),
                        Imss = reader.GetString(4),
                        SueldoSemanal = reader.GetDecimal(5),
                        Baja = reader.GetBoolean(6),
                        MotivoBaja = reader.GetString(7),
                        FechaIngreso = reader.GetString(8)
                    };

                    listaEmpleados.Add(empleado);
                }
                respuesta.ListaEmpleados = listaEmpleados;
                respuesta.CodRechazo = 0;
                respuesta.MsgRechazo = string.Empty;
            }
            catch (Exception ex)
            {
                respuesta.ListaEmpleados = null;
                respuesta.CodRechazo = 99;
                respuesta.MsgRechazo = ex.Message + "|" + ex.InnerException;
            }
            CerrarConexion();
            return respuesta;
        }

        [WebMethod]
        public RespuestaGuardarEmpleado ConsultarEmpleadoxId()
        {
            var respuesta = new RespuestaGuardarEmpleado();
            try
            {
                string query = "SELECT IdEmpleado" +
                                "       ,Nombre" +
                                "       ,Apellido" +
                                "       ,Categoria" +
                                "       ,Imss" +
                                "       ,SueldoSemanal" +
                                "       ,Baja" +
                                "       ,MotivoBaja" +
                                "       ,FechaIngreso " +
                                "FROM Empleado " +
                                "WHERE IdEmpleado = (SELECT MAX(IdEmpleado) FROM Empleado)";
                MySqlCommand cmd = new MySqlCommand(query, SqlConnection);
                MySqlDataReader reader = cmd.ExecuteReader();
                List<Empleado> listaEmpleados = new List<Empleado>();

                while (reader.Read())
                {
                    var empleado = new Empleado
                    {
                        IdEmpleado = reader.GetInt32(0),
                        Nombre = reader.GetString(1),
                        Apellido = reader.GetString(2),
                        Categoria = reader.GetString(3),
                        Imss = reader.GetString(4),
                        SueldoSemanal = reader.GetDecimal(5),
                        Baja = reader.GetBoolean(6),
                        MotivoBaja = reader.GetString(7),
                        FechaIngreso = reader.GetString(8)
                    };
                    respuesta.Empleado = empleado;
                }

                respuesta.Respuesta.CodRechazo = 0;
                respuesta.Respuesta.MsgRechazo = string.Empty;
            }
            catch (Exception ex)
            {
                respuesta.Empleado = null;
                respuesta.Respuesta.CodRechazo = 99;
                respuesta.Respuesta.MsgRechazo = "ConsultarEmpleadoxId " + ex.Message + "|" + ex.InnerException;
            }

            return respuesta;
        }

        [WebMethod]
        public RespuestaGuardarEmpleado GuardarEmpleado(Empleado empleado)
        {
            var respuesta = new RespuestaGuardarEmpleado
            {
                Respuesta = new RespuestaBase(),
                Empleado = new Empleado()
            };
            try
            {
                CrearConexionSql();
                AbrirConexion();

                MySqlCommand cmd = new MySqlCommand
                {
                    Connection = SqlConnection,
                    CommandText = "INSERT INTO Empleado " +
                                    "(Nombre, Apellido, Categoria, " +
                                    "Imss, SueldoSemanal, Baja, MotivoBaja, FechaIngreso) " +
                                    "VALUES (@Nombre, @Apellido, @Categoria, @Imss, " +
                                    "@SueldoSemanal, @Baja, @MotivoBaja, @FechaIngreso)"
                };

                cmd.Parameters.Add("@Nombre", MySqlDbType.VarChar).Value = empleado.Nombre;
                cmd.Parameters.Add("@Apellido", MySqlDbType.VarChar).Value = empleado.Apellido;
                cmd.Parameters.Add("@Categoria", MySqlDbType.VarChar).Value = empleado.Categoria;
                cmd.Parameters.Add("@Imss", MySqlDbType.VarChar).Value = empleado.Imss;
                cmd.Parameters.Add("@SueldoSemanal", MySqlDbType.Decimal).Value = empleado.SueldoSemanal;
                cmd.Parameters.Add("@Baja", MySqlDbType.VarChar).Value = empleado.Baja;
                cmd.Parameters.Add("@MotivoBaja", MySqlDbType.VarChar).Value = empleado.MotivoBaja;
                cmd.Parameters.Add("@FechaIngreso", MySqlDbType.VarChar).Value = empleado.FechaIngreso;

                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.RecordsAffected > 0)
                {
                    var insert = ConsultarEmpleadoxId();
                    respuesta.Empleado = insert.Empleado;
                    respuesta.Respuesta = insert.Respuesta;
                }
                CerrarConexion();
            }
            catch (Exception ex)
            {
                respuesta.Respuesta.CodRechazo = 99;
                respuesta.Respuesta.MsgRechazo = "GuardarEmpleado " + ex.Message;
                CerrarConexion();
            }
            return respuesta;
        }

        #endregion

        #region EmpleadoAsistencia

        [WebMethod]
        public List<EmpleadoAsistencia> GetEmpleadoAsistencias()
        {
            try
            {
                CrearConexionSql();
                AbrirConexion();
                string query = "SELECT e.IdEmpleado" +
                                "		,e.Nombre" +
                                "		,e.Apellido" +
                                "		,e.Categoria" +
                                "		,e.Imss" +
                                "		,e.SueldoSemanal" +
                                "		,e.Baja " +
                                "		,e.MotivoBaja" +
                                "		,e.FechaIngreso" +
                                "		,a.NoSemana" +
                                "		,a.Lun" +
                                "		,a.Mar" +
                                "		,a.Mie" +
                                "		,a.Jue" +
                                "		,a.Vie" +
                                "		,a.Sab" +
                                "       ,a.SemanaCompleta " +
                                "		,a.FechaInicio" +
                                "		,a.FechaFinal " +
                                "		,0 AS DiasTrabajados " +
                                "FROM Empleado e" +
                                "	LEFT JOIN HistorialAsistencia a" +
                                "		ON e.IdEmpleado = a.IdEmpleado ";

                MySqlCommand cmd = new MySqlCommand(query, SqlConnection);
                MySqlDataReader reader = cmd.ExecuteReader();

                List<EmpleadoAsistencia> listaEmpleadoAsistencia = new List<EmpleadoAsistencia>();

                while (reader.Read())
                {
                    var historialAsistencia = new EmpleadoAsistencia
                    {
                        IdEmpleado = reader.GetInt32(0),
                        Nombre = reader.GetString(1),
                        Apellido = reader.GetString(2),
                        Categoria = reader.GetString(3),
                        Imss = reader.GetString(4),
                        SueldoSemanal = reader.GetDecimal(5),
                        Baja = reader.GetBoolean(6),
                        MotivoBaja = reader.GetString(7),
                        FechaIngreso = reader.GetString(8),
                        NoSemana = reader.GetInt32(9),
                        Lun = reader.GetBoolean(10),
                        Mar = reader.GetBoolean(11),
                        Mie = reader.GetBoolean(12),
                        Jue = reader.GetBoolean(13),
                        Vie = reader.GetBoolean(14),
                        Sab = reader.GetBoolean(15),
                        SemanaCompleta = reader.GetBoolean(16),
                        FechaInicio = reader.GetString(17),
                        FechaFinal = reader.GetString(18)
                    };
                    listaEmpleadoAsistencia.Add(historialAsistencia);
                }

                CerrarConexion();
                return listaEmpleadoAsistencia;
            }
            catch (Exception ex)
            {
                CerrarConexion();
                throw ex;
            }
        }

        [WebMethod]
        public List<EmpleadoAsistencia> GetEmpleadoAsistenciasxSemana(int noSemana)
        {
            try
            {
                CrearConexionSql();
                AbrirConexion();
                string query = "SELECT e.IdEmpleado" +
                                "		,e.Nombre" +
                                "		,e.Apellido" +
                                "		,e.Categoria" +
                                "		,e.Imss" +
                                "		,e.SueldoSemanal" +
                                "		,e.Baja " +
                                "		,e.MotivoBaja" +
                                "		,e.FechaIngreso" +
                                "		,a.NoSemana" +
                                "		,a.Lun" +
                                "		,a.Mar" +
                                "		,a.Mie" +
                                "		,a.Jue" +
                                "		,a.Vie" +
                                "		,a.Sab" +
                                "       ,a.SemanaCompleta " +
                                "		,a.FechaInicio" +
                                "		,a.FechaFinal " +
                                "		,0 AS DiasTrabajados " +
                                "FROM Empleado e" +
                                "	LEFT JOIN HistorialAsistencia a" +
                                "		ON e.IdEmpleado = a.IdEmpleado " +
                                "WHERE	a.NoSemana = " + noSemana;

                MySqlCommand cmd = new MySqlCommand(query, SqlConnection);
                MySqlDataReader reader = cmd.ExecuteReader();

                List<EmpleadoAsistencia> listaEmpleadoAsistencia = new List<EmpleadoAsistencia>();

                while (reader.Read())
                {
                    var historialAsistencia = new EmpleadoAsistencia
                    {
                        IdEmpleado = reader.GetInt32(0),
                        Nombre = reader.GetString(1),
                        Apellido = reader.GetString(2),
                        Categoria = reader.GetString(3),
                        Imss = reader.GetString(4),
                        SueldoSemanal = reader.GetDecimal(5),
                        Baja = reader.GetBoolean(6),
                        MotivoBaja = reader.GetString(7),
                        FechaIngreso = reader.GetString(8),
                        NoSemana = reader.GetInt32(9),
                        Lun = reader.GetBoolean(10),
                        Mar = reader.GetBoolean(11),
                        Mie = reader.GetBoolean(12),
                        Jue = reader.GetBoolean(13),
                        Vie = reader.GetBoolean(14),
                        Sab = reader.GetBoolean(15),
                        SemanaCompleta = reader.GetBoolean(16),
                        FechaInicio = reader.GetString(17),
                        FechaFinal = reader.GetString(18),
                        DiasTrabajados = 0
                    };
                    listaEmpleadoAsistencia.Add(historialAsistencia);
                }

                CerrarConexion();
                return listaEmpleadoAsistencia;
            }
            catch (Exception ex)
            {
                CerrarConexion();
                throw ex;
            }
        }

        #endregion

        #region HistorialAsistencia

        [WebMethod]
        public HistorialAsistencia ConsultarAsistenciaxIdEmpleado(int noSemana, int idEmpleado)
        {
            try
            {
                CrearConexionSql();
                AbrirConexion();
                string query = "SELECT Id " +
                                "      ,IdEmpleado " +
                                "      ,NoSemana " +
                                "      ,Lun " +
                                "      ,Mar " +
                                "      ,Mie " +
                                "      ,Jue " +
                                "      ,Vie " +
                                "      ,Sab " +
                                "      ,SemanaCompleta " +
                                "      ,FechaInicio " +
                                "      ,FechaFinal " +
                                "FROM   HistorialAsistencia " +
                                "WHERE  NoSemana = " + noSemana + " " +
                                "AND    IdEmpleado = " + idEmpleado;

                MySqlCommand cmd = new MySqlCommand(query, SqlConnection);
                MySqlDataReader reader = cmd.ExecuteReader();
                HistorialAsistencia historialAsistencia = null;

                while (reader.Read())
                {
                    historialAsistencia = new HistorialAsistencia
                    {
                        Id = reader.GetInt32(0),
                        IdEmpleado = reader.GetInt32(1),
                        NoSemana = reader.GetInt32(2),
                        Lun = reader.GetBoolean(3),
                        Mar = reader.GetBoolean(4),
                        Mie = reader.GetBoolean(5),
                        Jue = reader.GetBoolean(6),
                        Vie = reader.GetBoolean(7),
                        Sab = reader.GetBoolean(8),
                        SemanaCompleta = reader.GetBoolean(9),
                        FechaInicio = reader.GetString(10),
                        FechaFinal = reader.GetString(11)
                    };
                }

                CerrarConexion();
                return historialAsistencia;
            }
            catch (Exception ex)
            {
                CerrarConexion();
                throw ex;
            }
        }

        [WebMethod]
        public List<HistorialAsistencia> ConsultarAsistenciaxSemana(int noSemana)
        {
            try
            {
                CrearConexionSql();
                AbrirConexion();
                string query = "SELECT DISTINCT * " +
                                "FROM HistorialAsistencia " +
                                "WHERE NoSemana = " + noSemana;
                MySqlCommand cmd = new MySqlCommand(query, SqlConnection);
                MySqlDataReader reader = cmd.ExecuteReader();
                List<HistorialAsistencia> listaHistorialAsistencia = new List<HistorialAsistencia>();

                while (reader.Read())
                {
                    var historialAsistencia = new HistorialAsistencia
                    {
                        Id = reader.GetInt32(0),
                        IdEmpleado = reader.GetInt32(1),
                        NoSemana = reader.GetInt32(2),
                        Lun = reader.GetBoolean(3),
                        Mar = reader.GetBoolean(4),
                        Mie = reader.GetBoolean(5),
                        Jue = reader.GetBoolean(6),
                        Vie = reader.GetBoolean(7),
                        Sab = reader.GetBoolean(8),
                        SemanaCompleta = reader.GetBoolean(9),
                        FechaInicio = reader.GetString(10),
                        FechaFinal = reader.GetString(11)
                    };
                    listaHistorialAsistencia.Add(historialAsistencia);
                }

                CerrarConexion();
                return listaHistorialAsistencia;
            }
            catch (Exception ex)
            {
                CerrarConexion();
                throw ex;
            }
        }

        [WebMethod]
        public bool GuardarAsistencia(HistorialAsistencia asistencia)
        {
            try
            {
                CrearConexionSql();
                AbrirConexion();

                MySqlCommand cmd = new MySqlCommand
                {
                    Connection = SqlConnection,
                    CommandText = "INSERT INTO HistorialAsistencia " +
                                    "(IdEmpleado, NoSemana, Lun, Mar, Mie, Jue, " +
                                    "Vie, Sab, SemanaCompleta, FechaInicio, FechaFinal) " +
                                    "VALUES (@IdEmpleado, @NoSemana, @Lun, @Mar, @Mie, " +
                                    "@Jue, @Vie, @Sab, @SemanaCompleta, @FechaInicio, @FechaFinal)"
                };
                cmd.Parameters.Add("@IdEmpleado", MySqlDbType.Int32).Value = asistencia.IdEmpleado;
                cmd.Parameters.Add("@NoSemana", MySqlDbType.Int32).Value = asistencia.NoSemana;
                cmd.Parameters.Add("@Lun", MySqlDbType.Int32).Value = asistencia.Lun;
                cmd.Parameters.Add("@Mar", MySqlDbType.Int32).Value = asistencia.Mar;
                cmd.Parameters.Add("@Mie", MySqlDbType.Int32).Value = asistencia.Mie;
                cmd.Parameters.Add("@Jue", MySqlDbType.Int32).Value = asistencia.Jue;
                cmd.Parameters.Add("@Vie", MySqlDbType.Int32).Value = asistencia.Vie;
                cmd.Parameters.Add("@Sab", MySqlDbType.Int32).Value = asistencia.Sab;
                cmd.Parameters.Add("@SemanaCompleta", MySqlDbType.Int32).Value = asistencia.SemanaCompleta;
                cmd.Parameters.Add("@FechaInicio", MySqlDbType.Int32).Value = asistencia.FechaInicio;
                cmd.Parameters.Add("@FechaFinal", MySqlDbType.Int32).Value = asistencia.FechaFinal;

                cmd.ExecuteNonQuery();

                CerrarConexion();
                return true;
            }
            catch (Exception ex)
            {
                CerrarConexion();
                return false;
                throw ex;
            }
        }

        [WebMethod]
        public string ActualizarAsistencia(HistorialAsistencia asistencia)
        {
            try
            {
                CrearConexionSql();
                AbrirConexion();
                var result = "";
                MySqlCommand cmd = new MySqlCommand
                {
                    Connection = SqlConnection,
                    CommandText = "UPDATE HistorialAsistencia " +
                                    "SET Lun = @Lun, " +
                                    "Mar = @Mar, " +
                                    "Mie = @Mie, " +
                                    "Jue = @Jue, " +
                                    "Vie = @Vie, " +
                                    "Sab = @Sab, " +
                                    "SemanaCompleta = @SemanaCompleta, " +
                                    "FechaInicio = @FechaInicio, " +
                                    "FechaFinal = @FechaFinal " +
                                    "WHERE  IdEmpleado = @IdEmpleado " +
                                    "AND    NoSemana = @NoSemana "
                };

                cmd.Parameters.Add("@IdEmpleado", MySqlDbType.Int32).Value = asistencia.IdEmpleado;
                cmd.Parameters.Add("@NoSemana", MySqlDbType.Int32).Value = asistencia.NoSemana;
                cmd.Parameters.Add("@Lun", MySqlDbType.Int32).Value = asistencia.Lun;
                cmd.Parameters.Add("@Mar", MySqlDbType.Int32).Value = asistencia.Mar;
                cmd.Parameters.Add("@Mie", MySqlDbType.Int32).Value = asistencia.Mie;
                cmd.Parameters.Add("@Jue", MySqlDbType.Int32).Value = asistencia.Jue;
                cmd.Parameters.Add("@Vie", MySqlDbType.Int32).Value = asistencia.Vie;
                cmd.Parameters.Add("@Sab", MySqlDbType.Int32).Value = asistencia.Sab;
                cmd.Parameters.Add("@SemanaCompleta", MySqlDbType.Int32).Value = asistencia.SemanaCompleta;
                cmd.Parameters.Add("@FechaInicio", MySqlDbType.VarChar).Value = asistencia.FechaInicio;
                cmd.Parameters.Add("@FechaFinal", MySqlDbType.VarChar).Value = asistencia.FechaFinal;

                //cmd.ExecuteNonQuery();

                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.RecordsAffected > 0)
                    result = "OK";
                else
                    result = "NO";

                CerrarConexion();
                return result;
            }
            catch (Exception ex)
            {
                CerrarConexion();
                return ex.Message;
                throw ex;
            }
        }

        [WebMethod]
        public bool GenerarHistorial(List<HistorialAsistencia> ListaAsistencia)
        {
            try
            {
                CrearConexionSql();
                var result = false;

                foreach (var asistencia in ListaAsistencia)
                {
                    AbrirConexion();

                    MySqlCommand cmd = new MySqlCommand
                    {
                        Connection = SqlConnection,
                        CommandText = "INSERT INTO HistorialAsistencia " +
                                        "(IdEmpleado, NoSemana, Lun, Mar, Mie, Jue, " +
                                        "Vie, Sab, SemanaCompleta, FechaInicio, FechaFinal) " +
                                        "VALUES (@IdEmpleado, @NoSemana, @Lun, @Mar, @Mie, " +
                                        "@Jue, @Vie, @Sab, @SemanaCompleta, @FechaInicio, @FechaFinal)"
                    };

                    cmd.Parameters.Add("@IdEmpleado", MySqlDbType.Int32).Value = asistencia.IdEmpleado;
                    cmd.Parameters.Add("@NoSemana", MySqlDbType.Int32).Value = asistencia.NoSemana;
                    cmd.Parameters.Add("@Lun", MySqlDbType.Int32).Value = asistencia.Lun;
                    cmd.Parameters.Add("@Mar", MySqlDbType.Int32).Value = asistencia.Mar;
                    cmd.Parameters.Add("@Mie", MySqlDbType.Int32).Value = asistencia.Mie;
                    cmd.Parameters.Add("@Jue", MySqlDbType.Int32).Value = asistencia.Jue;
                    cmd.Parameters.Add("@Vie", MySqlDbType.Int32).Value = asistencia.Vie;
                    cmd.Parameters.Add("@Sab", MySqlDbType.Int32).Value = asistencia.Sab;
                    cmd.Parameters.Add("@SemanaCompleta", MySqlDbType.Int32).Value = asistencia.SemanaCompleta;
                    cmd.Parameters.Add("@FechaInicio", MySqlDbType.VarChar).Value = asistencia.FechaInicio;
                    cmd.Parameters.Add("@FechaFinal", MySqlDbType.VarChar).Value = asistencia.FechaFinal;

                    MySqlDataReader reader = cmd.ExecuteReader();

                    if (reader.RecordsAffected > 0)
                        result = true;
                    else
                        result = false;
                    CerrarConexion();
                }

                return result;
            }
            catch (Exception ex)
            {
                CerrarConexion();
                return false;
                throw ex;
            }
        }

        #endregion

        #region Imagen
        [WebMethod]
        public List<Imagen> ConsultarImagenesTipo(int idEmpleado, int idTipoImagen)
        {
            try
            {
                CrearConexionSql();
                AbrirConexion();
                string query = "SELECT Id, IdEmpleado, RutaImagen " +
                                "FROM Imagen " +
                                "WHERE IdEmpleado = " + idEmpleado + " AND IdTipoImagen = " + idTipoImagen;
                MySqlCommand cmd = new MySqlCommand(query, SqlConnection);
                MySqlDataReader reader = cmd.ExecuteReader();
                List<Imagen> listaImagenes = new List<Imagen>();
                while (reader.Read())
                {
                    var imagen = new Imagen
                    {
                        Id = reader.GetInt32(0),
                        IdEmpleado = reader.GetInt32(1),
                        RutaImagen = reader.GetString(2)
                    };
                    listaImagenes.Add(imagen);
                }
                CerrarConexion();
                return listaImagenes;
            }
            catch (Exception ex)
            {
                CerrarConexion();
                throw ex;
            }
        }

        [WebMethod]
        public bool GuardarImagen(Imagen imagen)
        {
            try
            {
                CrearConexionSql();
                AbrirConexion();
                var result = false;

                MySqlCommand cmd = new MySqlCommand
                {
                    Connection = SqlConnection,
                    CommandText = "INSERT INTO Imagen " +
                                    "(IdEmpleado, RutaImagen, IdTipoImagen) " +
                                    "VALUES (@IdEmpleado, @RutaImagen, @IdTipoImagen)"
                };
                cmd.Parameters.Add("@IdEmpleado", MySqlDbType.Int32).Value = imagen.IdEmpleado;
                cmd.Parameters.Add("@RutaImagen", MySqlDbType.VarChar).Value = imagen.RutaImagen;
                cmd.Parameters.Add("@IdTipoImagen", MySqlDbType.Int32).Value = imagen.IdTipoImagen;

                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.RecordsAffected > 0)
                    result = true;
                else
                    result = false;

                CerrarConexion();
                return result;
            }
            catch (Exception ex)
            {
                CerrarConexion();
                return false;
                throw ex;
            }
        }

        #endregion

        #region Reporte

        [WebMethod]
        public List<Reporte> ConsultarReportes()
        {
            try
            {
                CrearConexionSql();
                AbrirConexion();
                string query = "SELECT * " +
                                "FROM Reporte ";
                MySqlCommand cmd = new MySqlCommand(query, SqlConnection);
                MySqlDataReader reader = cmd.ExecuteReader();
                List<Reporte> listaReportes = new List<Reporte>();

                while (reader.Read())
                {
                    var reporte = new Reporte
                    {
                        Id = reader.GetInt32(0),
                        Titulo = reader.GetString(1),
                        Icono = reader.GetString(2)
                    };

                    listaReportes.Add(reporte);
                }

                CerrarConexion();
                return listaReportes;
            }
            catch (Exception ex)
            {
                CerrarConexion();
                throw ex;
            }
        }

        [WebMethod]
        public bool GuardarReporte(Reporte reporte)
        {
            try
            {
                CrearConexionSql();
                AbrirConexion();
                var result = false;

                MySqlCommand cmd = new MySqlCommand
                {
                    Connection = SqlConnection,
                    CommandText = "INSERT INTO Reporte " +
                                    "(Titulo, Icono) " +
                                    "VALUES (@Titulo, @Icono)"
                };
                cmd.Parameters.Add("@Titulo", MySqlDbType.VarChar).Value = reporte.Titulo;
                cmd.Parameters.Add("@Icono", MySqlDbType.VarChar).Value = reporte.Icono;

                //cmd.ExecuteNonQuery();

                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.RecordsAffected > 0)
                    result = true;
                else
                    result = false;

                CerrarConexion();
                return result;
            }
            catch (Exception ex)
            {
                CerrarConexion();
                return false;
                throw ex;
            }
        }

        #endregion

        #region Log

        [WebMethod]
        public bool Log(Log log)
        {
            try
            {
                CrearConexionSql();
                AbrirConexion();
                var result = false;

                MySqlCommand cmd = new MySqlCommand
                {
                    Connection = SqlConnection,
                    CommandText = "INSERT INTO Log " +
                                    "(Title, Message, StackTrace, Source, FechaRegistro) " +
                                    "VALUES (@Title, @Message, @StackTrace, @Source, @FechaRegistro)"
                };
                cmd.Parameters.Add("@Title", MySqlDbType.VarChar).Value = log.Title;
                cmd.Parameters.Add("@Message", MySqlDbType.VarChar).Value = log.Message;
                cmd.Parameters.Add("@StackTrace", MySqlDbType.VarChar).Value = log.StackTrace;
                cmd.Parameters.Add("@Source", MySqlDbType.VarChar).Value = log.Source;
                cmd.Parameters.Add("@FechaRegistro", MySqlDbType.VarChar).Value = log.FechaRegistro;

                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.RecordsAffected > 0)
                    result = true;
                else
                    result = false;

                CerrarConexion();
                return result;
            }
            catch (Exception ex)
            {
                CerrarConexion();
                return false;
                throw ex;
            }
        }

        #endregion

        #region Session

        [WebMethod]
        public Session ConsultarSession()
        {
            try
            {
                CrearConexionSql();
                AbrirConexion();
                string query = "SELECT Id, Oficina, Rfc, " +
                                "RazonSocial, Direccion, " +
                                "Prefijo, VersionApp, " +
                                "FolioAlta, FolioEmpleado, " +
                                "CorreoJefe, CorreoContador " +
                                "FROM Session ";
                MySqlCommand cmd = new MySqlCommand(query, SqlConnection);
                MySqlDataReader reader = cmd.ExecuteReader();
                Session sessionModel = new Session();
                while (reader.Read())
                {
                    sessionModel.Id = reader.GetInt32(0);
                    sessionModel.Oficina = reader.GetString(1);
                    sessionModel.Rfc = reader.GetString(2);
                    sessionModel.RazonSocial = reader.GetString(3);
                    sessionModel.Direccion = reader.GetString(4);
                    sessionModel.Prefijo = reader.GetString(5);
                    sessionModel.VersionApp = reader.GetString(6);
                    sessionModel.FolioAlta = reader.GetInt32(7);
                    sessionModel.FolioEmpleado = reader.GetInt32(8);
                    sessionModel.CorreoJefe = reader.GetString(9);
                    sessionModel.CorreoContador = reader.GetString(10);
                }
                CerrarConexion();
                return sessionModel;
            }
            catch (Exception ex)
            {
                CerrarConexion();
                throw ex;
            }
        }

        [WebMethod]
        public bool GuardarSession(Session session)
        {
            try
            {
                CrearConexionSql();
                AbrirConexion();
                var result = false;

                MySqlCommand cmd = new MySqlCommand
                {
                    Connection = SqlConnection,
                    CommandText = "INSERT INTO Session " +
                                    "(Oficina, Rfc, RazonSocial, Direccion, Prefijo, VersionApp, " +
                                    "FolioAlta, FolioEmpleado, CorreoJefe, CorreoContador) " +
                                    "VALUES (@Oficina, @Rfc, @RazonSocial, @Direccion, @Prefijo, " +
                                    "@VersionApp, @FolioAlta, @FolioEmpleado, @CorreoJefe, @CorreoContador)"
                };
                cmd.Parameters.Add("@Oficina", MySqlDbType.VarChar).Value = session.Oficina;
                cmd.Parameters.Add("@Rfc", MySqlDbType.VarChar).Value = session.Rfc;
                cmd.Parameters.Add("@RazonSocial", MySqlDbType.VarChar).Value = session.RazonSocial;
                cmd.Parameters.Add("@Direccion", MySqlDbType.VarChar).Value = session.Direccion;
                cmd.Parameters.Add("@Prefijo", MySqlDbType.VarChar).Value = session.Prefijo;
                cmd.Parameters.Add("@VersionApp", MySqlDbType.VarChar).Value = session.VersionApp;
                cmd.Parameters.Add("@FolioAlta", MySqlDbType.Int32).Value = session.FolioAlta;
                cmd.Parameters.Add("@FolioEmpleado", MySqlDbType.Int32).Value = session.FolioEmpleado;
                cmd.Parameters.Add("@CorreoJefe", MySqlDbType.VarChar).Value = session.CorreoJefe;
                cmd.Parameters.Add("@CorreoContador", MySqlDbType.VarChar).Value = session.CorreoContador;

                //cmd.ExecuteNonQuery();

                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.RecordsAffected > 0)
                    result = true;
                else
                    result = false;

                CerrarConexion();
                return result;
            }
            catch (Exception ex)
            {
                CerrarConexion();
                return false;
                throw ex;
            }
        }

        [WebMethod]
        public bool ActualizarSession(Session session)
        {
            try
            {
                CrearConexionSql();
                AbrirConexion();
                var result = false;
                MySqlCommand cmd = new MySqlCommand
                {
                    Connection = SqlConnection,
                    CommandText = "UPDATE Session " +
                                    "SET Oficina = @Oficina, " +
                                    "Rfc = @Rfc, " +
                                    "RazonSocial = @RazonSocial, " +
                                    "Direccion = @Direccion, " +
                                    "Prefijo = @Prefijo, " +
                                    "VersionApp = @VersionApp, " +
                                    "FolioAlta = @FolioAlta, " +
                                    "FolioEmpleado = @FolioEmpleado, " +
                                    "CorreoJefe = @CorreoJefe, " +
                                    "CorreoContador = @CorreoContador "
                };
                cmd.Parameters.Add("@Oficina", MySqlDbType.VarChar).Value = session.Oficina;
                cmd.Parameters.Add("@Rfc", MySqlDbType.VarChar).Value = session.Rfc;
                cmd.Parameters.Add("@RazonSocial", MySqlDbType.VarChar).Value = session.RazonSocial;
                cmd.Parameters.Add("@Direccion", MySqlDbType.VarChar).Value = session.Direccion;
                cmd.Parameters.Add("@Prefijo", MySqlDbType.VarChar).Value = session.Prefijo;
                cmd.Parameters.Add("@VersionApp", MySqlDbType.VarChar).Value = session.VersionApp;
                cmd.Parameters.Add("@FolioAlta", MySqlDbType.Int32).Value = session.FolioAlta;
                cmd.Parameters.Add("@FolioEmpleado", MySqlDbType.Int32).Value = session.FolioEmpleado;
                cmd.Parameters.Add("@CorreoJefe", MySqlDbType.VarChar).Value = session.CorreoJefe;
                cmd.Parameters.Add("@CorreoContador", MySqlDbType.VarChar).Value = session.CorreoContador;

                //cmd.ExecuteNonQuery();

                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.RecordsAffected > 0)
                    result = true;
                else
                    result = false;

                CerrarConexion();
                return result;
            }
            catch (Exception ex)
            {
                CerrarConexion();
                return false;
                throw ex;
            }
        }

        #endregion

        #region Usuario

        [WebMethod]
        public Usuario ConsultarUsuario(string idUsuario, string contrasena)
        {
            Usuario usuarioModel = new Usuario();
            try
            {
                CrearConexionSql();
                AbrirConexion();
                string query = "SELECT Id, " +
                                "IdUsuario, " +
                                "Contrasena, " +
                                "NombreUsuario, " +
                                "Correo, " +
                                "Login " +
                                "FROM Usuario " +
                                "WHERE IdUsuario = '" + idUsuario + "' " +
                                "AND Contrasena = '" + contrasena + "' ";
                MySqlCommand cmd = new MySqlCommand(query, SqlConnection);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    usuarioModel.Id = reader.GetInt32(0);
                    usuarioModel.IdUsuario = reader.GetString(1);
                    usuarioModel.Contrasena = reader.GetString(2);
                    usuarioModel.NombreUsuario = reader.GetString(3);
                    usuarioModel.Correo = reader.GetString(4);
                    usuarioModel.Login = Convert.ToBoolean(reader.GetInt32(5));
                }
                CerrarConexion();
                usuarioModel.Exception = "";
            }
            catch (Exception ex)
            {
                CerrarConexion();
                usuarioModel.Exception = ex.Message;
            }
            return usuarioModel;
        }

        [WebMethod]
        public RespuestaIniciarSesion IniciarSesion(string idUsuario, string contrasena)
        {
            var respuesta = new RespuestaIniciarSesion
            {
                Respuesta = new RespuestaBase(),
                Usuario = new Usuario()
            };
            try
            {
                CrearConexionSql();
                AbrirConexion();
                string query = "SELECT Id, " +
                                "IdUsuario, " +
                                "Contrasena, " +
                                "NombreUsuario, " +
                                "Correo, " +
                                "Login " +
                                "FROM Usuario " +
                                "WHERE IdUsuario = '" + idUsuario + "' " +
                                "AND Contrasena = '" + contrasena + "' ";
                MySqlCommand cmd = new MySqlCommand(query, SqlConnection);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    respuesta.Usuario.Id = reader.GetInt32(0);
                    respuesta.Usuario.IdUsuario = reader.GetString(1);
                    respuesta.Usuario.Contrasena = "";// reader.GetString(2);
                    respuesta.Usuario.NombreUsuario = reader.GetString(3);
                    respuesta.Usuario.Correo = reader.GetString(4);
                    respuesta.Usuario.Login = Convert.ToBoolean(reader.GetInt32(5));
                }
                CerrarConexion();
                respuesta.Respuesta.CodRechazo = 0;
                respuesta.Respuesta.MsgRechazo = string.Empty;
            }
            catch (Exception ex)
            {
                CerrarConexion();
                respuesta.Respuesta.CodRechazo = 99;
                respuesta.Respuesta.MsgRechazo = ex.Message;
            }
            return respuesta;
        }

        [WebMethod]
        public Usuario ConsultarUsuarioxIdUsuario(string idUsuario)
        {
            try
            {
                CrearConexionSql();
                AbrirConexion();
                string query = "SELECT Id, " +
                                "IdUsuario, " +
                                "NombreUsuario, " +
                                "Correo," +
                                "Contrasena, " +
                                "Login " +
                                "FROM Usuario " +
                                "WHERE IdUsuario = '" + idUsuario + "' ";
                MySqlCommand cmd = new MySqlCommand(query, SqlConnection);
                MySqlDataReader reader = cmd.ExecuteReader();
                Usuario usuarioModel = new Usuario();
                while (reader.Read())
                {
                    usuarioModel.Id = reader.GetInt32(0);
                    usuarioModel.IdUsuario = reader.GetString(1);
                    usuarioModel.NombreUsuario = reader.GetString(2);
                    usuarioModel.Correo = reader.GetString(3);
                    usuarioModel.Contrasena = reader.GetString(4);
                    usuarioModel.Login = Convert.ToBoolean(reader.GetInt32(5));
                }
                CerrarConexion();
                return usuarioModel;
            }
            catch (Exception ex)
            {
                CerrarConexion();
                throw ex;
            }
        }

        [WebMethod]
        public bool ActualizarEstatusUsuario(bool login, string idUsuario)
        {
            try
            {
                var result = false;
                CrearConexionSql();
                AbrirConexion();
                string query = "UPDATE Usuario " +
                                "SET Login = " + Convert.ToInt32(login) + " " +
                                "WHERE IdUsuario = '" + idUsuario + "'";
                MySqlCommand cmd = new MySqlCommand(query, SqlConnection);
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.RecordsAffected > 0)
                    result = true;
                else
                    result = false;

                CerrarConexion();
                return result;
            }
            catch (Exception ex)
            {
                CerrarConexion();
                throw ex;
            }
        }

        #endregion
    }
}
