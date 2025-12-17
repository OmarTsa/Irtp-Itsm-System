using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Irtp.Itsm.Core.Entities
{
    // Mapeo de la tabla de ROLES (Ej: Administrador, Soporte)
    [Table("SEG_ROLES")]
    public class Rol
    {
        [Key]
        [Column("ID_ROL")]
        public int Id { get; set; }

        [Column("NOMBRE")]
        [Required]
        public string Nombre { get; set; } = string.Empty;
    }

    // Mapeo de la tabla de AREAS (Ej: Gerencia General, OTIC)
    [Table("SEG_AREAS")]
    public class Area
    {
        [Key]
        [Column("ID_AREA")]
        public int Id { get; set; }

        [Column("NOMBRE")]
        public string Nombre { get; set; } = string.Empty;
    }

    // Mapeo de la tabla de USUARIOS (Personal del IRTP)
    [Table("SEG_USUARIOS")]
    public class Usuario
    {
        [Key]
        [Column("ID_USUARIO")]
        public int Id { get; set; }

        [Column("DNI")]
        public string? Dni { get; set; }

        [Column("NOMBRES")]
        public string Nombres { get; set; } = string.Empty;

        [Column("APELLIDOS")]
        public string Apellidos { get; set; } = string.Empty;

        [Column("CORREO")]
        public string Correo { get; set; } = string.Empty;

        // Nombre de usuario para el Login (Ej: otito)
        [Column("USERNAME")]
        public string NombreUsuario { get; set; } = string.Empty;

        // Contraseña encriptada
        [Column("PASSWORD_HASH")]
        public string ClaveEncriptada { get; set; } = string.Empty;

        // Relación con Rol
        [Column("ID_ROL")]
        public int IdRol { get; set; }

        [ForeignKey("IdRol")]
        public virtual Rol? Rol { get; set; }

        // Relación con Área
        [Column("ID_AREA")]
        public int? IdArea { get; set; }

        [ForeignKey("IdArea")]
        public virtual Area? Area { get; set; }

        [Column("ESTADO")]
        public int Estado { get; set; } // 1: Activo, 0: Inactivo

        // Propiedad calculada (No se guarda en BD, solo sirve para mostrar en pantalla)
        [NotMapped]
        public string NombreCompleto => $"{Nombres} {Apellidos}";
    }
}