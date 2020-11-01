using SCHOOLCONTROL.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SCHOOLCONTROL.Services.DAO
{
    public class CourseStudentDAO : IDisposable
    {
        AdminContext queryCtx;
        public CourseStudentDAO()
        {
            queryCtx = new AdminContext();
        }

        public void Dispose()
        {
            if (queryCtx != null)
                queryCtx.Dispose();
        }

        public object Register(CURSOESTUDIANTE model)
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

        public object Modify(CURSOESTUDIANTE model)
        {
            try
            {
                using (var ctx = new AdminContext())
                {
                    var entry = ctx.CursosEstudiantes.Where(e => e.ID == model.ID).First();
                    entry.CALIFICACION = model.CALIFICACION;

                    ctx.Entry(entry).State = System.Data.Entity.EntityState.Modified;
                    return ctx.SaveChanges();

                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public object Delete(CURSOESTUDIANTE model)
        {
            try
            {
                using (var ctx = new AdminContext())
                {
                    var entry = ctx.CursosEstudiantes.Where(e => e.ID == model.ID).First();
                    ctx.Entry(entry).State = System.Data.Entity.EntityState.Deleted;
                    return ctx.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }



        public IQueryable<CURSOESTUDIANTE> Query(Expression<Func<CURSOESTUDIANTE, bool>> predicate)
        {
            return queryCtx.CursosEstudiantes.Where(predicate);
        }

    }
}
