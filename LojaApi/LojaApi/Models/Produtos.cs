﻿namespace LojaApi.Models
{
   
        public class Produtos
        {
            public int Id { get; set; }
            public string Nome { get; set; }
            public string Descricao { get; set; }
            public decimal Preco { get; set; }
            public int QuantidadeEstoque { get; set; }
        }

}

