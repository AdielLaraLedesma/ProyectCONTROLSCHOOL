using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using SCHOOLCONTROL.Services.Models;

namespace SCHOOLCONTROL.Services.DAO
{
    public class TeacherDAO : IDisposable
    {
        AdminContext queryCtx;
        public TeacherDAO()
        {
            queryCtx = new AdminContext();
        }
        public void Dispose()
        {
            if (queryCtx != null)
                queryCtx.Dispose();
        }
        public object Register(PROFESOR model)
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

        public object Modify(PROFESOR model)
        {
            try
            {
                using (var ctx = new AdminContext())
                {
                    var entry = ctx.Profesores.Where(e => e.ID == model.ID).First();
                    entry.NOMBRE = model.NOMBRE;
                    entry.APELLIDOS = model.APELLIDOS;
                    entry.DIRECCION = model.DIRECCION;
                    entry.SALARIO = model.SALARIO;
                    entry.TIENEPLAZA = model.TIENEPLAZA;
                    ctx.Entry(entry).State = System.Data.Entity.EntityState.Modified;
                    return ctx.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public object Delete(PROFESOR model)
        {
            try
            {
                using (var ctx = new AdminContext())
                {
                    var entry = ctx.Profesores.Where(e => e.ID == model.ID).First();
                    ctx.Entry(entry).State = System.Data.Entity.EntityState.Deleted;
                    var ctxs = ctx.SaveChanges();
                    return ctx.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public IQueryable<PROFESOR> Query(Expression<Func<PROFESOR, bool>> predicate)
        {
            return queryCtx.Profesores.Where(predicate);
        }
    }
}
