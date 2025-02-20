using Academia.Translogix.WebApi._Features.Gral.Dtos;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Gral;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Academia.Translogix.WebApi._Features.Gral.Services
{
    public class MonedaService
    {

        private readonly TranslogixDBContext _translogixDBContext;
        private readonly IMapper _mapper;

        public MonedaService(TranslogixDBContext translogixDBContext, IMapper mapper)
        {
            _translogixDBContext = translogixDBContext; 
            _mapper = mapper;
        }

        #region MONEDAS
        public List<MonedasDto> ObtenerMonedas()
        {
            List<MonedasDto> monedas = new ();
            var result = _translogixDBContext.Monedas.AsQueryable().AsNoTracking().ToList();

            foreach (var item in result)
            {
                monedas.Add(_mapper.Map<MonedasDto>(item));
            }


            //result.ForEach(moneda => monedas.Add(_mapper.Map<MonedasDto>(moneda)));

            //result.SelectMany(mone => ( monedas.Add(_mapper.Map<MonedasDto>(mone)) ) ).to;

            return monedas;

        }

        public bool InsertarMoneda(MonedasDtoInsertar modelo)
        {
            var moneda = _mapper.Map<Monedas>(modelo);

            var result = _translogixDBContext.Monedas.Add(moneda);

            return _translogixDBContext.SaveChanges() > 0;
        }

        public bool ActualizarMoneda(int id, MonedasDtoActualizar monedaDto)
        {
            var monedaExistente = _translogixDBContext.Monedas.FirstOrDefault(x => x.moneda_id == id);

            if (monedaExistente == null)
                return false;

            _mapper.Map(monedaDto, monedaExistente);

            _translogixDBContext.Monedas.Update(monedaExistente);

            return _translogixDBContext.SaveChanges() > 0;
        }

        public bool EliminarMoneda(int id, bool es_activo)
        {
            var monedaExistente = _translogixDBContext.Monedas.FirstOrDefault(x => x.moneda_id == id);
            if (monedaExistente == null)
                return false;

            monedaExistente.es_activo = es_activo;

            _translogixDBContext.Entry(monedaExistente)
                                 .Property(x => x.es_activo)
                                 .IsModified = true;

            return _translogixDBContext.SaveChanges() > 0;
        }

        public MonedasDto ObtenerMonedaPorId(int id)
        {
            var moneda = _translogixDBContext.Monedas
                .AsNoTracking()
                .FirstOrDefault(x => x.moneda_id == id);

            return _mapper.Map<MonedasDto>(moneda);
        }

        #endregion

    }
}
