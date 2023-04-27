using MySql.Data.MySqlClient;
using ProjetoVeterinaria.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ProjetoVeterinaria.Dados
{
    public class clRacaAcoes
    {
        Conexao con = new Conexao();

        public void inserirRaca(clRaca cm)
        {
            MySqlCommand cmd = new MySqlCommand("call pcd_insertRaca(@nomeRaca, @codTipo)", con.conectarBD());

            //"insert into tblRaca(nomeRaca, codTipo) values(@nomeRaca, @codTipo)"

            cmd.Parameters.Add("@nomeRaca", MySqlDbType.VarChar).Value = cm.nomeRaca;
            cmd.Parameters.Add("@codTipo", MySqlDbType.VarChar).Value = cm.codTipo;

            cmd.ExecuteNonQuery();
            con.desconectarBD();
        }

        public List<clRaca> buscarRaca()
        {
            List<clRaca> racaList = new List<clRaca>();
            MySqlCommand cmd = new MySqlCommand("call pcd_selectRaca();", con.conectarBD());

            //"select * from tblRaca"

            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            da.Fill(dt);
            con.desconectarBD();

            foreach (DataRow dr in dt.Rows)
            {
                racaList.Add(
                    new clRaca
                    {
                        codRaca = Convert.ToInt32(dr["codRaca"]),
                        nomeRaca = Convert.ToString(dr["nomeRaca"]),
                        tipo = Convert.ToString(dr["tipo"])
                    }
                    );
            }
            return racaList;
        }
    }
}