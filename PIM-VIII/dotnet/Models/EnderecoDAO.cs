using System.Collections.Generic;
using Microsoft.Data.Sqlite;

namespace trabalho.Models
{
  public class EnderecoDAO : IGenericDAO<Endereco>
  {
    private List<Endereco> Enderecos =  new List<Endereco>();
    private SqliteConnection connection = Connection.getConnection();

    public Endereco get(int id){
      var command = connection.CreateCommand();
      command.CommandText =  @"select * from endereco where id=$id";
      command.Parameters.AddWithValue("$id", id);

      var reader = command.ExecuteReader();
      if(reader.Read()) {

          var id_endereco = reader.GetInt16(0);
          var logradouro = reader.GetString(1);
          var numero = reader.GetInt16(2);
          var cep = reader.GetString(3);
          var bairro = reader.GetString(4);
          var cidade = reader.GetString(5);
          var estado = reader.GetString(6);

          var endereco = new Endereco(id_endereco, logradouro, numero,
              cep, bairro, cidade, estado);

          return endereco;
      }

      return null;
    }


    public void remove(int id) {
      var command = connection.CreateCommand();
      command.CommandText =  @"delete from endereco where id=$id";
      command.Parameters.AddWithValue("$id", id);
      command.ExecuteNonQuery();
    }

    public void update(Endereco entity){
    }

    public void create(Endereco entity) {
      var command = connection.CreateCommand();
      command.CommandText =  @"insert
        into endereco (logradouro, numero, cep, bairro, cidade, estado) values(
            $logradouro, $numero, $cep, $bairro, $cidade, $estado);";

      command.Parameters.AddWithValue("$logradouro", entity.logradouro);
      command.Parameters.AddWithValue("$numero", entity.numero);
      command.Parameters.AddWithValue("$cep", entity.cep);
      command.Parameters.AddWithValue("$bairro", entity.bairro);
      command.Parameters.AddWithValue("$cidade", entity.cidade);
      command.Parameters.AddWithValue("$estado", entity.estado);

      command.ExecuteNonQuery();

    }


    public EnderecoDAO()
    {
      connection.Open();
    }
    ~EnderecoDAO()
    {
      connection.Close();
    }

    public List<Endereco>  getAll() {
      var command = connection.CreateCommand();

      command.CommandText = @"SELECT * FROM endereco;";
      using ( var reader = command.ExecuteReader()) {
        while (reader.Read()) {

          var id = reader.GetInt16(0);
          var logradouro = reader.GetString(1);
          var numero = reader.GetInt16(2);
          var cep = reader.GetString(3);
          var bairro = reader.GetString(4);
          var cidade = reader.GetString(5);
          var estado = reader.GetString(6);

          var endereco = new Endereco(id, logradouro, numero,
              cep, bairro, cidade, estado);

          Enderecos.Add(endereco);
        }
      }

      return Enderecos;
    }
  }
}


