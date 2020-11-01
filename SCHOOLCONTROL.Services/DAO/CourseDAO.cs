using SCHOOLCONTROL.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SCHOOLCONTROL.Services.DAO
{
    public class CourseDAO : IDisposable
    {
        AdminContext queryCtx;
        public CourseDAO()
        {
            queryCtx = new AdminContext();
        }

        public void Dispose()
        {
            if (queryCtx != null)
                queryCtx.Dispose();
        }

        public object Register(CURSO model)
        {
            try
            {
                using (var ctx = new AdminContext())
                {
                    ctx.Entry(model).State = System.Data.Entity.EntityState.Added;
                    return ctx.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public object Modify(CURSO model)
        {
            try
            {
                using (var ctx = new AdminContext())
                {
                    var entry = ctx.Cursos.Where(e => e.ID == model.ID).First();
                    entry.NOMBRE = model.NOMBRE;
                    entry.GRADO = model.GRADO;
                    entry.IDPROFESOR = model.IDPROFESOR;

                    ctx.Entry(entry).State = System.Data.Entity.EntityState.Modified;
                    return ctx.SaveChanges();

                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public object Delete(CURSO model)
        {
            try
            {
                using (var ctx = new AdminContext())
                {
                    var entry = ctx.Cursos.Where(e => e.ID == model.ID).First();
                    ctx.Entry(entry).State = System.Data.Entity.EntityState.Deleted;
                    return ctx.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IQueryable<CURSO> Query(Expression<Func<CURSO, bool>> predicate)
        {
            return queryCtx.Cursos.Where(predicate);
        }

    }
}
