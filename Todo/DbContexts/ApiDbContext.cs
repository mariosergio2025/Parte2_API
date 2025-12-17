using Microsoft.EntityFrameworkCore;
using System;
using System.IO;

namespace Todo.DbContexts
{
    public class ApiDbContext :DbContext
    {
        /* DbContext - funciona como um portal para o Banco de Dados
         * Dbset - funciona como um portal para as tabelas, sendo que cada tabela tem Dbset */
        public DbSet<TodoItem> Todos => Set<TodoItem>();
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),"TodoBanco.db");
            optionsBuilder.UseSqlite($"Data Source={path}");
            
            base.OnConfiguring(optionsBuilder);
        }
    }
}
