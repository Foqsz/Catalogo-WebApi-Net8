﻿using Microsoft.EntityFrameworkCore;
using WebApiCatalogo.Catalogo.Core.Model;
using WebApiCatalogo.Catalogo.Infrastucture.Context;

namespace WebApiCatalogo.Catalogo.Infrastucture.Repository
{
    public class CategoriaRepository : Repository<CategoriaModel>, ICategoriaRepository //herda de Repository e implementa ICategoriaRepository
    { 
        private readonly AppDbContext _context;

        public CategoriaRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        //Listar Categorias
        //public IEnumerable<CategoriaModel> GetCategorias()
        //{
        //    var categorias = _context.Categorias.ToList(); 
        //    return categorias;
        //}

        ////Listar Categoria por ID
        //public CategoriaModel GetCategoriaPorId(int id)
        //{
        //    return _context.Categorias.FirstOrDefault(c => c.CategoriaId == id); //para checar id usa-se FirstOrDefault com lambda
        //}
         
        ////Criar Categoria
        //public CategoriaModel GetCategoriaCriar(CategoriaModel categoria)
        //{
        //    if(categoria == null)
        //    {
        //        throw new ArgumentNullException(nameof(categoria));
        //    }
        //    _context.Add(categoria);
        //    _context.SaveChanges();

        //    return categoria;
        //}

        ////Atualizar uma categoria
        //public CategoriaModel GetCategoriaUpdate(CategoriaModel categoria)
        //{
        //    if (categoria == null)
        //    {
        //        throw new ArgumentNullException(nameof(categoria));
        //    }

        //    _context.Entry(categoria).State = EntityState.Modified;
        //    _context.SaveChanges();
        //    return categoria;
        //}

        ////Deletar uma categoria por ID
        //public CategoriaModel GetCategoriaDelete(int id)
        //{
        //    var categoria = _context.Categorias.Find(id);

        //    if (categoria == null)
        //    {
        //        throw new ArgumentNullException(nameof(categoria));
        //    }

        //    _context.Categorias.Remove(categoria);
        //    _context.SaveChanges();

        //    return categoria;
        //}
    }
}