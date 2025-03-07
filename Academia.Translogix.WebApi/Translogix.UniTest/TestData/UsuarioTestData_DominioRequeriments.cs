using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Academia.Translogix.WebApi._Features.Acce.Requirement;
using Academia.Translogix.WebApi._Features.Gral.Requeriment;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Acce;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Gral;
using Microsoft.VisualStudio.TestPlatform.Common.Utilities;

namespace Translogix.UniTest.TestData
{
    public class UsuarioTestData_DominioRequeriments : TheoryData<Usuarios, UsuariosDomainRequirement, bool>
    {

        public UsuarioTestData_DominioRequeriments()
        {
            Add(UsuarioCorrecto(), RequirementCorrecto(), true);
            Add(UsuarioNombreLargo(), RequirementCorrecto(), false);
            Add(UsuarioCorrecto(), RequirementRolExistenteFalse(), false);
            Add(UsuarioNombreLargo(), RequirementColaboradorExistenteFalse(), false);
        }

        public Usuarios UsuarioCorrecto()
            => new Usuarios
            {
                clave = [], 
                nombre = "Madian"
            };

        public Usuarios UsuarioNombreLargo()
            => new Usuarios
            {
                clave = [],
                nombre = "Madia".PadRight(151, 'n')
            };

        public UsuariosDomainRequirement RequirementCorrecto(Action<UsuariosDomainRequirement>? configure = null)
        {
            var requirement = new UsuariosDomainRequirement
            {
                RolExistente = true,
                ColaboradorExistente = true
            };

            configure?.Invoke(requirement);
            return requirement;
        }

        public UsuariosDomainRequirement RequirementRolExistenteFalse()
            => RequirementCorrecto(x => x.RolExistente = false);

        public UsuariosDomainRequirement RequirementColaboradorExistenteFalse()
            => RequirementCorrecto(x => x.ColaboradorExistente = false);
    }
}
