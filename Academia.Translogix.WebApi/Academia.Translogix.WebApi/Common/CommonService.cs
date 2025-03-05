using Academia.Translogix.WebApi.Infrastructure;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Gral;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Viaj;
using Farsiman.Domain.Core.Standard.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Academia.Translogix.WebApi.Common
{
    public class CommonService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CommonService(UnitOfWorkBuilder unitOfWork)
        {
            _unitOfWork = unitOfWork.BuildDbTranslogix();
        }
        public bool IdentidadIgual(string identidad)
        {
            return !(from perso in _unitOfWork.Repository<Personas>().AsQueryable().AsNoTracking()
                     where perso.identidad == identidad
                     select perso).Any();
        }

        public bool CorreoIgual(string correo)
        {
            return !(from perso in _unitOfWork.Repository<Personas>().AsQueryable().AsNoTracking()
                     where perso.correo_electronico == correo
                     select perso).Any();
        }

        public bool EntidadExistente<T>(int id)
            where T : class
        {
            string pkName = "";
            var properties = typeof(T).GetProperties();
            foreach (var property in properties)
            {
                bool isKey = property.GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.KeyAttribute), false).Length > 0;
                if (isKey)
                {
                    pkName = property.Name;
                    break;
                }
            }

            return _unitOfWork.Repository<T>().AsQueryable().AsNoTracking()
                        .Any(x => EF.Property<int>(x, pkName) == id);
        }
    }
}
