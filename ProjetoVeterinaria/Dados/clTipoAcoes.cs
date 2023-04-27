using MySql.Data.MySqlClient;
using ProjetoVeterinaria.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ProjetoVeterinaria.Dados
{
    public class clTipoAcoes
    {
        Conexao con = new Conexao();

        public void inserirTipo(clTipo cm)
        {
            MySqlCommand cmd = new MySqlCommand("call pcd_insertTipo(@tipo)", con.conectarBD());
            //"insert into tblTipo(tipo) values(@tipo)"

            cmd.Parameters.Add("@tipo", MySqlDbType.VarChar).Value = cm.tipo;

            cmd.ExecuteNonQuery();
            con.desconectarBD();
        }

        public List<clTipo> buscarTipo()
        {
            List<clTipo> tipoList = new List<clTipo>();
            MySqlCommand cmd = new MySqlCommand("call pcd_selectTipo();", con.conectarBD());

            //"select * from tblTipo"

            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            da.Fill(dt);
            con.desconectarBD();

            foreach (DataRow dr in dt.Rows)
            {
                tipoList.Add(
                    new clTipo
                    {
                        codTipo = Convert.ToInt32(dr["codTipo"]),
                        tipo = Convert.ToString(dr["tipo"])
                    }
                    );
            }
            return tipoList;
        }
    }
}