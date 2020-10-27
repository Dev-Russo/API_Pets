﻿using API_Pets.Context;
using API_Pets.Domains;
using API_Pets.Interfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Pets.Repositories
{
    public class RacaRepository : IRaca
    {
        //conectar com o banco com o context
        PetContext conexao = new PetContext();

        SqlCommand cmd = new SqlCommand();

        public Raca Alterar(Raca r)
        {
            //abre conexao
            cmd.Connection = conexao.Conectar();
            //metodos
            cmd.CommandText = "UPDATE Raca SET Descricao= @descricao, IdTipoPet=@idtipopet WHERE IdRaca = @id";
            cmd.Parameters.AddWithValue("@descricao", r.Descricao);
            cmd.Parameters.AddWithValue("@idtipodepet", r.IdTipoPet);
            //executta
            cmd.ExecuteNonQuery();

            //feha conexao
            conexao.Desconectar();
            //retorna
            return r;
        }

        public Raca BuscarPorId(int id)
        {
            //abre conexao
            cmd.Connection = conexao.Conectar();
            //metodo
            cmd.CommandText = "SELECT * FROM Raca WHERE IdRaca = @id";
            cmd.Parameters.AddWithValue("@id", id);
            //executa
            SqlDataReader dados = cmd.ExecuteReader();
            Raca raca = new Raca();

            while (dados.Read())
            {
                raca.IdRaca = Convert.ToInt32(dados.GetValue(0));
                raca.Descricao = dados.GetValue(1).ToString();
                raca.IdTipoPet = Convert.ToInt32(dados.GetValue(2));
            }

            //fecha conexao
            conexao.Desconectar();
            return raca;
        }

        public Raca Cadastrar(Raca r)
        {
            //abre conexao
            cmd.Connection = conexao.Conectar();

            cmd.CommandText = "INSERT INTO Raca (Descricao, IdTipoPet)" +
                "VALUES" +
                "(@descricao, @idtipopet)";
            cmd.Parameters.AddWithValue("@descricao", r.Descricao);
            cmd.Parameters.AddWithValue("@idtipopet", r.IdTipoPet);

            //executa metodos
            cmd.ExecuteNonQuery();

            //fecha conexao
            conexao.Desconectar();

            return r;
        }

        public void Excluir(int id)
        {
            //abre conexao
            cmd.Connection = conexao.Conectar();

            //metodos
            cmd.CommandText = "DELETE FROM Raca WHERE IdRaca = @id";
            cmd.Parameters.AddWithValue("@id", id);

            //executa os metodos
            cmd.ExecuteNonQuery();

            //fecha conexao
            conexao.Desconectar();
        }

        public List<Raca> LerTodos()
        {
            //abre conexao 
            cmd.Connection = conexao.Conectar();

            //query
            cmd.CommandText = "SELECT *FROM Raca";

            SqlDataReader dados = cmd.ExecuteReader();

            //cria a lista para guardar tipopet
            List<Raca> racaa = new List<Raca>();

            while (dados.Read())
            {
                racaa.Add(
                    new Raca()
                    {
                        IdRaca = Convert.ToInt32(dados.GetValue(0)),
                        Descricao = dados.GetValue(1).ToString(),
                        IdTipoPet = Convert.ToInt32(dados.GetValue(2)),
                    }
                    );
            }
            //laço


            //fecha conexao 
            conexao.Desconectar();

            return racaa;
        }
    }
}
