using System.Data;
using System.Numerics;
using CRUDfinal.Models;
using Microsoft.Data.SqlClient;

namespace CRUDfinal.Repositories
{
    public class RepositorySala
    {
        private DataTable tablaSalas;
        SqlConnection cn;
        SqlCommand com;

        public RepositorySala()
        {
            string connectionString = @"Data Source=LOCALHOST\SQLEXPRESS;Initial Catalog=HOSPITAL;Persist Security Info=True;User ID=sa;Encrypt=True;Trust Server Certificate=True";
            string sql = "select * from SALA";
            SqlDataAdapter adDoc = new SqlDataAdapter(sql, connectionString);
            this.tablaSalas = new DataTable();
            adDoc.Fill(this.tablaSalas);

            //para consultas adonet
            this.cn = new SqlConnection(connectionString);
            this.com = new SqlCommand();
            this.com.Connection = this.cn;
        }

        public List<Sala> GetSalas()
        {
            var consulta = from datos in this.tablaSalas.AsEnumerable() select datos;
            List<Sala> salas = new List<Sala>();
            foreach (var row in consulta)
            {
                Sala sala = new Sala();

                sala.Hospital_cod = row.Field<int>("HOSPITAL_COD");
                sala.Sala_cod = row.Field<int>("SALA_COD");
                sala.Nombre = row.Field<string>("NOMBRE");
                sala.Numcama = row.Field<int>("NUM_CAMA");
                salas.Add(sala);
            }
            return salas;
        } 

        public Sala DetalleSala(int Sala_cod)
        {
            var consulta = from datos in this.tablaSalas.AsEnumerable() where datos.Field<int>("SALA_COD") == Sala_cod select datos;
            var row = consulta.First();
            Sala sala = new Sala
            {
                Hospital_cod = row.Field<int>("HOSPITAL_COD"),
                Sala_cod = row.Field<int>("SALA_COD"),
                Nombre = row.Field<string>("NOMBRE"),
                Numcama = row.Field<int>("NUM_CAMA"),
            };
            return sala;
        }

        public async Task DeleteAsync(int Sala_cod)
        {
            string connectionString = @"Data Source=LOCALHOST\SQLEXPRESS;Initial Catalog=HOSPITAL;Persist Security Info=True;User ID=sa;Encrypt=True;Trust Server Certificate=True";
            string sql = "delete from SALA where SALA_COD=@Sala_cod";

            this.cn = new SqlConnection(connectionString);
            this.com = new SqlCommand();
            this.com.Connection = cn;
            this.com.Parameters.AddWithValue("@Sala_cod", Sala_cod);
            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;
            await this.cn.OpenAsync();
            await this.com.ExecuteNonQueryAsync();
            await this.cn.CloseAsync();
            this.com.Parameters.Clear();
        }

        public async Task CreateSalaAsync(int Hospital_cod, int Sala_cod, string Nombre, string Numcama)
        {
            string connectionString = @"Data Source=LOCALHOST\SQLEXPRESS;Initial Catalog=HOSPITAL;Persist Security Info=True;User ID=sa;Encrypt=True;Trust Server Certificate=True";
            string sql = "insert into SALA values(@Hospital_cod, @Sala_cod, @Nombre, @Numcama)";
            this.cn = new SqlConnection(connectionString);
            this.com = new SqlCommand();
            this.com.Connection = cn;
            this.com.Parameters.AddWithValue("@Hospital_cod", Hospital_cod);
            this.com.Parameters.AddWithValue("@Sala_cod", Sala_cod);
            this.com.Parameters.AddWithValue("@Nombre", Nombre);
            this.com.Parameters.AddWithValue("@Numcama", Numcama);
            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;
            await this.cn.OpenAsync();
            await this.com.ExecuteNonQueryAsync();
            await this.cn.CloseAsync();
            this.com.Parameters.Clear();
        }

        public async Task UpdateSalasAsync(int Hospital_cod, int Sala_cod, string Nombre, string Numcama)
        {
            string sql = "update SALA set HOSPITAL_COD=@Hospital_cod, SALA_COD=@Sala_cod, NOMBRE=@Nombre, NUM_CAMA=@Numcama where SALA_COD=@Sala_cod";
            this.com.Parameters.AddWithValue("@Hospital_cod", Hospital_cod);
            this.com.Parameters.AddWithValue("@Sala_cod", Sala_cod);
            this.com.Parameters.AddWithValue("@Nombre", Nombre);
            this.com.Parameters.AddWithValue("@Numcama", Numcama);
            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;
            await this.cn.OpenAsync();
            await this.com.ExecuteNonQueryAsync();
            await this.cn.CloseAsync();
            this.com.Parameters.Clear();
        }

        public List<int> GetIdHospitales()
        {
            var consulta = (from datos in this.tablaSalas.AsEnumerable() select datos.Field<int>("HOSPITAL_COD")).Distinct();
            return consulta.ToList();
        }

        public List<Sala> GetSalaIdHopsital(int Hospital_cod)
        {
            var consulta = from datos in this.tablaSalas.AsEnumerable() where datos.Field<int>("HOSPITAL_COD") == Hospital_cod select datos;
            List<Sala> salas = new List<Sala>();
            foreach (var row in consulta)
            {
                Sala sala = new Sala
                {
                    Hospital_cod = row.Field<int>("HOSPITAL_COD"),
                    Sala_cod = row.Field<int>("SALA_COD"),
                    Nombre = row.Field<string>("NOMBRE"),
                    Numcama = row.Field<int>("NUM_CAMA"),
                };
                salas.Add(sala);
            }
            return salas;
        }
    }
}
