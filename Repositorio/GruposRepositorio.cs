using d_angela_variedades.Data;
using d_angela_variedades.Entidades;
using d_angela_variedades.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace d_angela_variedades.Repositorio
{
    public class GruposRepositorio : IGruposRepositorio
    {
        private readonly ApplicationDbContext context;

        public GruposRepositorio(ApplicationDbContext context)
        {
            this.context = context;
        }
        public Task<bool> EditarGrupo(GrupoDTO grupoDTO, int grupoId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EliminarGrupo(int grupoId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> GrupoExiste(int grupoId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> GuardarGrupo(GrupoDTO grupoDTO, int empresaId)
        {
            var grupo = new Grupos()
            {
                NombreGrupo = grupoDTO.NombreGrupo,
                EmpresaId = empresaId
            };

            context.Add(grupo);

            return await Save();
        }

        public async Task<Grupos> ObtenerGrupoAEditar(int grupoId)
        {
            var grupo = await context.Grupos.FirstOrDefaultAsync(grupo => grupo.GrupoId == grupoId);

            return grupo;
        }

        public async Task<List<Grupos>> ObtenerGrupos(int empresaId)
        {
            var grupos = await context.Grupos.Where(grupo => grupo.EmpresaId == empresaId).ToListAsync();

            return grupos;
        }

        public async Task<bool> Save()
        {
            var saved = await context.SaveChangesAsync();

            return saved > 0;
        }
    }
}
