using c19_38_BackEnd.Dtos;
using c19_38_BackEnd.Modelos;
using System.Runtime.CompilerServices;

namespace c19_38_BackEnd.Map
{
    public static class Mapper
    {

        // Model: RegistroDTO
        public static Usuario MapRegistroDtoToUsuario(this RegistroDto registroDto)
        {
            return new Usuario
            {
                Nombre = registroDto.Nombre,
                Apellido = registroDto.Apellido,
                Altura = registroDto.Altura,
                Peso = registroDto.Peso,
                FechaDeNac = registroDto.FechaNacimiento,
                Disciplina = registroDto.Disciplina,
                Genero = registroDto.Genero,
                Email = registroDto.Email,
                UserName = registroDto.Nombre.Replace(" ",""),
            };
        }


        //  Model: USUARIO
        public static UsuarioDto MapUsuarioToUsuarioDto(this Usuario usuario)
        {
            return new UsuarioDto
            {
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Genero = usuario.Genero,
                FechaDeNac = usuario.FechaDeNac,
                Peso = usuario.Peso,
                Altura = usuario.Altura,
                ActividadFisica = usuario.ActividadFisica,
                MediaUrl = usuario.MediaUrl,
                Disciplina = usuario.Disciplina
            };
        }
        public static Usuario MapUsuarioDtoToUsuario(this UsuarioDto usuario)
        {
            return new Usuario
            {
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Genero = usuario.Genero,
                FechaDeNac = usuario.FechaDeNac,
                Peso = usuario.Peso,
                Altura = usuario.Altura,
                ActividadFisica = usuario.ActividadFisica,
                MediaUrl = usuario.MediaUrl,
                Disciplina = usuario.Disciplina
            };
        }

        public static List<Usuario>MapListUsuarioDtoToListUsuario(this List<UsuarioDto> usuario)
        {
            var list = usuario.Select(u => u.MapUsuarioDtoToUsuario()).ToList(); return list;
        }

        public static List<UsuarioDto>MapListUsuarioToListUsuarioDto(this List<Usuario> usuario)
        {
            var list = usuario.Select(u => u.MapUsuarioToUsuarioDto()).ToList(); return list;
        }

        //  Model: SERIE
        public static SerieDto MapSerieToSerieDto( this Serie serie)
        {
            return new SerieDto
            {
                IdSerie = serie.IdSerie,
                Descripcion = serie.Descripcion,
                CantidadRepeticiones = serie.CantidadRepeticiones,
                CantidadSeries = serie.CantidadSeries,
                TiempoDescanso  = serie.TiempoDescanso,
                Duracion = serie.Duracion,
                IdEjercicio = serie.IdEjercicio,
                IdPlan = serie.IdPlan
            };
        }

        public static Serie MapSerieDtoToSerie( this SerieDto serie )
        {
            return new Serie
            {
                IdSerie = serie.IdSerie,
                Descripcion = serie.Descripcion,
                CantidadRepeticiones = serie.CantidadRepeticiones,
                CantidadSeries = serie.CantidadSeries,
                TiempoDescanso = serie.TiempoDescanso,
                Duracion = serie.Duracion,
                IdEjercicio = serie.IdEjercicio,
                IdPlan = serie.IdPlan
            };
        }
        public static List<Serie> MapListSerieDtoToSerie(this List<SerieDto> serieList)
        {
            var list = serieList.Select(s=> s.MapSerieDtoToSerie()).ToList(); return list;
        }

        public static List<SerieDto>MapListSerieToSerieDto( this List<Serie> serieList)
        {
            var list = serieList.Select(s =>s.MapSerieToSerieDto()).ToList(); return list;
        }

        //  Model: POST
        public static PostDto MapPostToPostDto( this Post post)
        {
            return new PostDto
            {
                IdPost = post.IdPost,
                Titulo = post.Titulo,
                Cuerpo = post.Cuerpo,
                FechaPublicacion = post.FechaPublicacion,
                MediaUrl = post.MediaUrl,
                IdAutorUsuario = post.IdAutorUsuario
            };
        }
        public static Post MapPostDtoToPost( this PostDto post)
        {
            return new Post
            {
                IdPost = post.IdPost,
                Titulo = post.Titulo,
                Cuerpo = post.Cuerpo,
                FechaPublicacion = post.FechaPublicacion,
                MediaUrl = post.MediaUrl,
                IdAutorUsuario = post.IdAutorUsuario
            };
        }
        public static List<Post> MapListPostDtoToPost(this List<PostDto> post)
        {
            var list = post.Select(p => p.MapPostDtoToPost()).ToList(); return list;
        }
        public static List<PostDto>MapListPostToPostDto(this List<Post> post)
        {
            var list = post.Select(p => p.MapPostToPostDto()).ToList(); return list;
        }

        //  Model: PLAN DE ENTRENAMIENTO
        public static PlanDeEntrenamientoDto MapPlanDeEntretamientoToPlanDeEntrenamientoDto(this PlanDeEntrenamiento plan)
        {
            return new PlanDeEntrenamientoDto
            {
                IdPlan = plan.IdPlan,
                Descripcion = plan.Descripcion,
                FechaPublicacion = plan.FechaPublicacion,
                IdAutorUsuario = plan.IdAutorUsuario,
                Nivel = plan.Nivel,
                TipoDisciplina = plan.TipoDisciplina
            };
        }
        public static PlanDeEntrenamiento MapPlanDeEntrenamientoDtoToPlanDeEntrenamiento( this PlanDeEntrenamientoDto plan)
        {
            return new PlanDeEntrenamiento
            {
                IdPlan = plan.IdPlan,
                Descripcion = plan.Descripcion,
                FechaPublicacion = plan.FechaPublicacion,
                IdAutorUsuario = plan.IdAutorUsuario,
                Nivel = plan.Nivel,
                TipoDisciplina = plan.TipoDisciplina
            };
        }
        public static List<PlanDeEntrenamiento> MapListPlanDeEntrenamientoDtoToListPlanDeEntrenamiento(this List<PlanDeEntrenamientoDto> plan)
        {
            var list = plan.Select(p => p.MapPlanDeEntrenamientoDtoToPlanDeEntrenamiento()).ToList(); return list;
        }
        public static List<PlanDeEntrenamientoDto> MapListPlanDeEntrenamientoToListPlanDeEntrenamientoDto(this List<PlanDeEntrenamiento> plan)
        {
            var list = plan.Select(p => p.MapPlanDeEntretamientoToPlanDeEntrenamientoDto()).ToList(); return list;
        }

        //  Model: HISTORIAL DE RENDIMIENTO
        public static HistorialRendimientoDto MapHistorialRendimientoToHistorialRendimientoDto(this HistorialRendimiento historialRendimiento)
        {
            return new HistorialRendimientoDto
            {
               IdHistorial = historialRendimiento.IdHistorial,
               cantFlexionesMin = historialRendimiento.cantFlexionesMin,
               cantSentadillasMin = historialRendimiento.cantSentadillasMin,
               FechaGuardado = historialRendimiento.FechaGuardado,
               CienMtsTiempo = historialRendimiento.CienMtsTiempo,
               IdUsuario = historialRendimiento.IdUsuario,
               Nota = historialRendimiento.Nota
            };
        }
        public static HistorialRendimiento MapHistorialRendimientoDtoToHistorialRendimiento(this HistorialRendimientoDto historialRendimiento)
        {
            return new HistorialRendimiento
            {
                IdHistorial = historialRendimiento.IdHistorial,
                cantFlexionesMin = historialRendimiento.cantFlexionesMin,
                cantSentadillasMin = historialRendimiento.cantSentadillasMin,
                FechaGuardado = historialRendimiento.FechaGuardado,
                CienMtsTiempo = historialRendimiento.CienMtsTiempo,
                IdUsuario = historialRendimiento.IdUsuario,
                Nota = historialRendimiento.Nota
            };
        }
        
        public static List<HistorialRendimiento> MapHistorialRendimientoDtoToHistorialRendimiento(this List<HistorialRendimientoDto> historial)
        {
            var list = historial.Select(l => l.MapHistorialRendimientoDtoToHistorialRendimiento()).ToList(); return list;   
        }

        public static List<HistorialRendimientoDto> MapHistorialRendimientoToHistorialRendimientoDto(this List<HistorialRendimiento> historial)
        {
            var list = historial.Select(l => l.MapHistorialRendimientoToHistorialRendimientoDto()).ToList(); return list;
        }

        //  Model: EJERCICIO

        public static EjercicioDto MapEjercicioToEjercicioDto(this Ejercicio ejercicio)
        {
            return new EjercicioDto
            {
                IdEjercicio = ejercicio.IdEjercicio,
                Descripcion = ejercicio.Descripcion,
                MediaUrl = ejercicio.MediaUrl,
                MusculoPrincipal = ejercicio.MusculoPrincipal,
                MusculoSecundario = ejercicio.MusculoSecundario
            };
        }

        public static Ejercicio MapEjercicioDtoToEjercicio(this EjercicioDto ejercicio)
        {
            return new Ejercicio
            {
                IdEjercicio = ejercicio.IdEjercicio,
                Descripcion = ejercicio.Descripcion,
                MediaUrl = ejercicio.MediaUrl,
                MusculoPrincipal = ejercicio.MusculoPrincipal,
                MusculoSecundario = ejercicio.MusculoSecundario
            };
        }

        public static List<Ejercicio> MapListEjercicioDtoToListEjercicio(this List<EjercicioDto> ejercicio)
        {
            var list =  ejercicio.Select(e => e.MapEjercicioDtoToEjercicio()).ToList(); return list;
        }

        public static List<EjercicioDto> MapListEjercicioToListEjercicioDto(this List<Ejercicio> ejercicio)
        {
            var list = ejercicio.Select(e => e.MapEjercicioToEjercicioDto()).ToList(); return list;
        }

        //  Model: COMENTARIOS

        public static ComentarioDto MapComentarioToComentarioDto(this Comentario comentario)
        {
            return new ComentarioDto
            {
                IdComentario = comentario.IdComentario,
                Cuerpo = comentario.Cuerpo,
                FechaPublicacion = comentario.FechaPublicacion,
                IdAutor = comentario.IdAutor,
                IdPost = comentario.IdPost
            };
        }

        public static Comentario MapComentarioDtoToComentario(this ComentarioDto comentario)
        {
            return new Comentario
            {
                IdComentario = comentario.IdComentario,
                Cuerpo = comentario.Cuerpo,
                FechaPublicacion = comentario.FechaPublicacion,
                IdAutor = comentario.IdAutor,
                IdPost = comentario.IdPost
            };
        }

        public static List<Comentario>MapListComentarioDtoToListComentario(this List<ComentarioDto> comentarioList)
        {
            var list = comentarioList.Select(c => c.MapComentarioDtoToComentario()).ToList(); return list;
        }

        public static List<ComentarioDto> MapListComentarioToListComentarioDto(this List<Comentario> comentarioList)
        {
            var list = comentarioList.Select(c => c.MapComentarioToComentarioDto()).ToList(); return list;
        }

        //  Model: BIBLIOTECA PLAN USUARIO

        public static BibliotecaPlanUsuarioDto MapBibliotecaPlanUsuarioToBibliotecaPlanUsuarioDto(this BibliotecaPlanUsuario biblioteca)
        {
            return new BibliotecaPlanUsuarioDto()
            {
                IdBiblioteca = biblioteca.IdBiblioteca,
                FechaGuardado = biblioteca.FechaGuardado,
                IdPlan = biblioteca.IdPlan,
                IdUsuario = biblioteca.IdUsuario
            };
        }

        public static BibliotecaPlanUsuario MapBibliotecaPlanUsuarioDtoToBibliotecaPlanUsuario(this BibliotecaPlanUsuarioDto biblioteca)
        {
            return new BibliotecaPlanUsuario()
            {
                IdBiblioteca = biblioteca.IdBiblioteca,
                FechaGuardado = biblioteca.FechaGuardado,
                IdPlan = biblioteca.IdPlan,
                IdUsuario = biblioteca.IdUsuario
            };
        }

        public static List<BibliotecaPlanUsuario>MapListBibliotecaPlanUsuarioDtoToListBibliotecaPlanUsuario(this List<BibliotecaPlanUsuarioDto> biblioteca)
        {
            var list = biblioteca.Select(b => b.MapBibliotecaPlanUsuarioDtoToBibliotecaPlanUsuario()).ToList(); return list;
        }

        public static List<BibliotecaPlanUsuarioDto> MapListBibliotecaPlanUsuarioToListBibliotecaPlanUsuarioDto(this List<BibliotecaPlanUsuario> biblioteca)
        {
            var list = biblioteca.Select(b => b.MapBibliotecaPlanUsuarioToBibliotecaPlanUsuarioDto()).ToList(); return list;
        }
    }
}
