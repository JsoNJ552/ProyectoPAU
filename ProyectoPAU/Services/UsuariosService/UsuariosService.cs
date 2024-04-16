﻿using Microsoft.EntityFrameworkCore;
using ProyectoPAU.Models;

namespace ProyectoPAU.Services.UsuariosService
{
    public class UsuariosService : IUsuariosService
    {

        private readonly TiendauContext _context;

        public UsuariosService(TiendauContext context)
        {
            _context = context;
        }


        public async Task EditarUsuario(Usuario usuario)
        {
            try
            {

                if(usuario!=null)
                {
                     _context.Update(usuario);
                    await _context.SaveChangesAsync();


                }


            }catch (Exception ex)
            {


            }
        }

        public async Task EliminarUsuario(string email )
        {
            try
            {
                Usuario usuarioDesactivar = await _context.Usuarios.Where(x=>x.Email.Equals(email)).FirstAsync();
                usuarioDesactivar.Activo = false;
                 _context.Usuarios.Update(usuarioDesactivar);
                await _context.SaveChangesAsync();

            }catch (Exception ex)
            {

            }
        }

        public async Task<List<Usuario>> obtenerUsuarios()
        {
            try
            {
                var usuarios = await _context.Usuarios.ToListAsync();
                return usuarios;

            }catch (Exception ex) { 
            }
            throw new NotImplementedException();
        }

        public async  Task<Usuario> obtenerUsuarioPorEmail(Usuario email)
        {
            try
            {

                var usuario = await _context.Usuarios.Include(x=> x.Rol). FirstOrDefaultAsync(x => x.Email == email.Email);

                if (usuario != null)
                {

                    return usuario;

                }



            }
            catch (Exception ex)
            {

                

            }

            return null;

        }

        public async Task<Usuario> obtenerUsuarioPorId(int IdUsuario)
        {
            try
            {

               Usuario usuario= await _context.Usuarios.FirstOrDefaultAsync(x=> x.UsuarioId == IdUsuario);

                if (usuario != null)
                {

                    return usuario;

                }

                

            }catch (Exception ex) { 
            
            }

            return null;
        }

        public Task<IEnumerable<Usuario>> obtenerUsuariosFiltro(Func<Usuario, bool> filtro = null)
        {
            throw new NotImplementedException();
        }

        public Task RegistrarUsuario(Usuario usuario, IFormFile photoFile)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Rol>> obtenerRoles()
        {

            try
            {

                var roles = await _context.Rols.ToListAsync();
                return roles;

            }catch (Exception ex)
            {


            }
            throw new NotImplementedException();
        }
    }
}
