﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace c19_38_BackEnd.Modelos
{
    public class PlanDeEntrenamiento
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPlan { get; set; }
        public string Descripcion { get; set; }
        public Disciplina TipoDisciplina { get; set; }
        public Nivel Nivel { get; set; }
        public DateTime FechaPublicacion { get; set; }
        public int IdAutorUsuario { get; set; }
        [ForeignKey(nameof(IdAutorUsuario))]
        public Usuario AutorUsuario { get; set; }
    }

    public enum Nivel
    {
        Principiante,
        Intermedio,
        Avanzado
    }
}
