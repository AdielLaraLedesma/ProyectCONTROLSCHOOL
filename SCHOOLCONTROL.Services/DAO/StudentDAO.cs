using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using SCHOOLCONTROL.Services.Models;

namespace SCHOOLCONTROL.Services.DAO 
{
   
    public class StudentDAO : IDisposable
    {
        AdminContext queryCtx;

        public StudentDAO()
        {
            queryCtx = new AdminContext();
        }

        public void Dispose()
        {
            if(queryCtx != null){
                queryCtx.Dispose();
            }
        }

        public object Register(ESTUDIANTE model)
        {
            try
            {
                using (var ctx = new AdminContext())
                {
                    ctx.Entry(model).State = System.Data.Entity.EntityState.Added;
                    return ctx.SaveChanges();
                }
            }
            catch(Exception)
            {
                throw;
            }
        }

        public object Modify(ESTUDIANTE model)
        {
            try
            {
                using(var ctx = new AdminContext())
                {
                    var entry = ctx.Estudiantes.Where(e => e.ID == model.ID).First();
                    entry.NOMBRE = model.NOMBRE;
                    entry.APELLIDOS = model.APELLIDOS;
                    entry.DIRECCION = model.DIRECCION;
                    //entry.CALIFICACION = model.CALIFICACION;
                    ctx.Entry(entry).State = System.Data.Entity.EntityState.Modified;
                    return ctx.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public object Delete(ESTUDIANTE model)
        {
            try
            {
                using (var ctx = new AdminContext())
                {
                    var entry = ctx.Estudiantes.Where(e => e.ID == model.ID).First();
                    ctx.Entry(entry).State = System.Data.Entity.EntityState.Deleted;
                    return ctx.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IQueryable<ESTUDIANTE> Query (Expression<Func<ESTUDIANTE,bool>> predicate)
        {
            return queryCtx.Estudiantes.Where(predicate);
        }
    }
}
