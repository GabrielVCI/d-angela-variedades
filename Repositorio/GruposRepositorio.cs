using d_angela_variedades.Data;
using d_angela_variedades.Entidades;
using d_angela_variedades.Interfaces;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<bool> EditarGrupo(GrupoDTO grupoDTO, int grupoId)
        {
            var grupo = await context.Grupos.FirstOrDefaultAsync(gru => gru.GrupoId == grupoId);

            grupo.NombreGrupo = grupoDTO.NombreGrupo;

            context.Update(grupo);

            return await Save();
        }

        public async Task<bool> EliminarGrupo(int grupoId, int empresaId)
        {
            var grupo = await context.Grupos.FirstOrDefaultAsync(gru => gru.GrupoId == grupoId && gru.EmpresaId == empresaId);

            context.Remove(grupo);

            return await Save();
        }

        public async Task<bool> GrupoExiste(int grupoId)
        {
            var grupoExiste = await context.Grupos.AnyAsync(gru => gru.GrupoId == grupoId);

            return grupoExiste; 
        }

        public async Task<bool> GrupoPerteneceAlaEmpresa(int grupoId, int empresaId)
        {
            var grupoPerteneceAlaEmpresa = await context.Grupos.AnyAsync(gru => gru.GrupoId == grupoId && gru.EmpresaId == empresaId);

            return grupoPerteneceAlaEmpresa;
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

        public async Task<Grupos> ObtenerGrupoAEditar(int grupoId, int empresaId)
        {
            var grupo = await context.Grupos.FirstOrDefaultAsync(grupo => grupo.GrupoId == grupoId && grupo.EmpresaId == empresaId);

            return grupo;
        }

        public async Task<List<Grupos>> ObtenerGrupoPorElNombre(string nombreGrupo, int empresaId)
        {
            var grupo = await context.Grupos.Where(gru => gru.NombreGrupo == nombreGrupo && gru.EmpresaId == empresaId).ToListAsync();

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
