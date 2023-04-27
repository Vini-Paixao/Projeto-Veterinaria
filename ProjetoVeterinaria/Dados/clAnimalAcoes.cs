using MySql.Data.MySqlClient;
using ProjetoVeterinaria.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ProjetoVeterinaria.Dados
{
    public class clAnimalAcoes
    {
        Conexao con = new Conexao();

        public void inserirAnimal(clAnimal cm)
        {
            MySqlCommand cmd = new MySqlCommand("call pcd_insertAnimal(@nomeAnimal, @codRaca, @codCliente)", con.conectarBD());

            //"insert into tblAnimal(nomeAnimal, codRaca, codCliente)" +
            //    " values(@nomeAnimal, @codRaca, @codCliente)"

            cmd.Parameters.Add("@nomeAnimal", MySqlDbType.VarChar).Value = cm.nomeAnimal;
            cmd.Parameters.Add("@codRaca", MySqlDbType.VarChar).Value = cm.codRaca;
            cmd.Parameters.Add("@codCliente", MySqlDbType.VarChar).Value = cm.codCliente;

            cmd.ExecuteNonQuery();
            con.desconectarBD();
        }

        public List<clAnimal> buscarAnimal()
        {
            List<clAnimal> vetList = new List<clAnimal>();
            MySqlCommand cmd = new MySqlCommand("call pcd_selectAnimal();", con.conectarBD());

            //"select * from tblAnimal"

            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            da.Fill(dt);
            con.desconectarBD();

            foreach (DataRow dr in dt.Rows)
            {
                vetList.Add(
                    new clAnimal
                    {
                        codAnimal = Convert.ToInt32(dr["codAnimal"]),
                        nomeAnimal = Convert.ToString(dr["nomeAnimal"]),
                        nomeRaca = Convert.ToString(dr["nomeRaca"]),
                        nomeCliente = Convert.ToString(dr["nomeCliente"])
                    }
                    );
            }
            return vetList;
        }

        public DataTable ConsAnimal(clAnimal cm)
        {
            MySqlCommand cmd = new MySqlCommand("call pcd_selectAnimal_porCod(@codAnimal);", con.conectarBD());

            cmd.Parameters.Add("@codAnimal", MySqlDbType.VarChar).Value = cm.codAnimal;
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable animal = new DataTable();
            da.Fill(animal);
            con.desconectarBD();
            return animal;
        }

        public void atualizarAnimal(clAnimal cm)
        {
            MySqlCommand cmd = new MySqlCommand("call pcd_updateAnimal(@codAnimal, @nomeAnimal, @codRaca, @codCliente)", con.conectarBD());

            //"update tblAnimal set nomeAnimal=@nomeAnimal," +
            //" codRaca=@codRaca, codCliente=@codCliente where codAnimal=@codAnimal"

            cmd.Parameters.Add("@codAnimal", MySqlDbType.VarChar).Value = cm.codAnimal;
            cmd.Parameters.Add("@nomeAnimal", MySqlDbType.VarChar).Value = cm.nomeAnimal;
            cmd.Parameters.Add("@codRaca", MySqlDbType.VarChar).Value = cm.codRaca;
            cmd.Parameters.Add("@codCliente", MySqlDbType.VarChar).Value = cm.codCliente;

            cmd.ExecuteNonQuery();
            con.desconectarBD();
        }

        public void deletarAnimal(int cod)
        {
            MySqlCommand cmd = new MySqlCommand("call pcd_deleteAnimal(@codAnimal)", con.conectarBD());

            //"delete from tblAnimal where codAnimal=@codAnimal"

            cmd.Parameters.AddWithValue("@codAnimal", cod);
            int i = cmd.ExecuteNonQuery();
            con.desconectarBD();
        }
    }
}