using MySql.Data.MySqlClient;
using ProjetoVeterinaria.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ProjetoVeterinaria.Dados
{
    public class clAtendimentoAcoes
    {
        Conexao con = new Conexao();

        public void inserirAtendimento(clAtendimento cm)
        {
            MySqlCommand cmd = new MySqlCommand("call pcd_insertAtendimento(@dataAtendimento, @horaAtendimento," +
                " @statusAtendimento, @codVeterinario, @codAnimal)", con.conectarBD());

            //"insert into tblAtendimento(dataAtendimento, horaAtendimento, statusAtendimento, codVeterinario, codAnimal)" +
            //    " values(@dataAtendimento, @horaAtendimento, @statusAtendimento, @codVeterinario, @codAnimal)"

            cmd.Parameters.Add("@dataAtendimento", MySqlDbType.VarChar).Value = cm.dataAtendimento;
            cmd.Parameters.Add("@horaAtendimento", MySqlDbType.VarChar).Value = cm.horaAtendimento;
            cmd.Parameters.Add("@statusAtendimento", MySqlDbType.VarChar).Value = cm.statusAtendimento;
            cmd.Parameters.Add("@codVeterinario", MySqlDbType.VarChar).Value = cm.codVeterinario;
            cmd.Parameters.Add("@codAnimal", MySqlDbType.VarChar).Value = cm.codAnimal;

            cmd.ExecuteNonQuery();
            con.desconectarBD();
        }

        public List<clAtendimento> buscarAtendimento()
        {
            List<clAtendimento> vetList = new List<clAtendimento>();
            MySqlCommand cmd = new MySqlCommand("call pcd_selectAtendimento();", con.conectarBD());

            //"select * from tblAtendimento"

            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            da.Fill(dt);
            con.desconectarBD();

            foreach (DataRow dr in dt.Rows)
            {
                vetList.Add(
                    new clAtendimento
                    {
                        codAtendimento = Convert.ToInt32(dr["codAtendimento"]),
                        dataAtendimento = Convert.ToString(dr["dataAtendimento"]),
                        horaAtendimento = Convert.ToString(dr["horaAtendimento"]),
                        statusAtendimento = Convert.ToString(dr["statusAtendimento"]),
                        nomeVeterinario = Convert.ToString(dr["nomeVeterinario"]),
                        nomeAnimal = Convert.ToString(dr["nomeAnimal"])
                    }
                    );
            }
            return vetList;
        }

        public void atualizarAtendimento(clAtendimento cm)
        {
            MySqlCommand cmd = new MySqlCommand("call pcd_updateAtendimento(@codAtendimento, @dataAtendimento, @horaAtendimento," +
                " @statusAtendimento, @codVeterinario, @codAnimal)", con.conectarBD());

            //"update tblAtendimento set nomeAtendimento=@nomeAtendimento," +
            //" codRaca=@codRaca, codCliente=@codCliente where codAtendimento=@codAtendimento"

            cmd.Parameters.Add("@codAtendimento", MySqlDbType.VarChar).Value = cm.codAtendimento;
            cmd.Parameters.Add("@dataAtendimento", MySqlDbType.VarChar).Value = cm.dataAtendimento;
            cmd.Parameters.Add("@horaAtendimento", MySqlDbType.VarChar).Value = cm.horaAtendimento;
            cmd.Parameters.Add("@statusAtendimento", MySqlDbType.VarChar).Value = cm.statusAtendimento;
            cmd.Parameters.Add("@codVeterinario", MySqlDbType.VarChar).Value = cm.codVeterinario;
            cmd.Parameters.Add("@codAnimal", MySqlDbType.VarChar).Value = cm.codAnimal;

            cmd.ExecuteNonQuery();
            con.desconectarBD();
        }

        public void deletarAtendimento(int cod)
        {
            MySqlCommand cmd = new MySqlCommand("call pcd_deleteAtendimento(@codAtendimento)", con.conectarBD());

            //"delete from tblAtendimento where codAtendimento=@codAtendimento"

            cmd.Parameters.AddWithValue("@codAtendimento", cod);
            int i = cmd.ExecuteNonQuery();
            con.desconectarBD();
        }
    }
}