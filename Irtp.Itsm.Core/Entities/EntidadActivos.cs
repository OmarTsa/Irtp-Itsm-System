using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Irtp.Itsm.Core.Entities
{
    // 1. Tipos de activos (Ej: Laptop, PC, Impresora)
    [Table("ACT_TIPOS_ACTIVO")]
    public class TipoActivo
    {
        [Key][Column("ID_TIPO")] public int Id { get; set; }
        [Column("NOMBRE")] public string Nombre { get; set; } = string.Empty;
    }

    // 2. Catálogo de Software (Para controlar licencias y versiones)
    [Table("ACT_CATALOGO_SOFTWARE")]
    public class CatalogoSoftware
    {
        [Key][Column("ID_SOFTWARE")] public int Id { get; set; }
        [Column("NOMBRE")] public string Nombre { get; set; } = string.Empty;
        [Column("VERSION")] public string? Version { get; set; }
        [Column("FECHA_FIN_SOPORTE")] public DateTime? FechaFinSoporte { get; set; }

        // Lógica de Negocio: ¿Es obsoleto? (Si la fecha actual > fin de soporte)
        [NotMapped]
        public bool EsObsoleto => FechaFinSoporte.HasValue && FechaFinSoporte.Value < DateTime.Now;
    }

    // 3. El inventario físico principal
    [Table("ACT_INVENTARIO")]
    public class ActivoTecnologico
    {
        [Key][Column("ID_ACTIVO")] public int Id { get; set; }

        [Column("CODIGO_PATRIMONIAL")]
        public string CodigoPatrimonial { get; set; } = string.Empty;

        [Column("ID_TIPO")] public int IdTipo { get; set; }
        [ForeignKey("IdTipo")] public virtual TipoActivo? Tipo { get; set; }

        [Column("MARCA")] public string? Marca { get; set; }
        [Column("MODELO")] public string? Modelo { get; set; }
        [Column("SERIE")] public string? Serie { get; set; }

        [Column("FECHA_COMPRA")] public DateTime? FechaCompra { get; set; }
        [Column("ESTADO_OPERATIVO")] public string? EstadoOperativo { get; set; } // Bueno, Malo
        [Column("UBICACION_FISICA")] public string? UbicacionFisica { get; set; }

        [Column("ID_USUARIO_ASIGNADO")] public int? IdUsuarioAsignado { get; set; }
        [ForeignKey("IdUsuarioAsignado")] public virtual Usuario? UsuarioAsignado { get; set; }

        // --- LÓGICA DE NEGOCIO PARA LA TESIS (CÁLCULO DE ANTIGÜEDAD) ---

        [NotMapped]
        public int AntiguedadAnios
        {
            get
            {
                if (!FechaCompra.HasValue) return 0;
                var hoy = DateTime.Today;
                var edad = hoy.Year - FechaCompra.Value.Year;
                if (FechaCompra.Value.Date > hoy.AddYears(-edad)) edad--;
                return edad;
            }
        }

        // Regla: Si tiene más de 6 años, es Brecha Tecnológica
        [NotMapped]
        public bool TieneBrechaTecnologica => AntiguedadAnios > 6;

        // Color para el Semáforo en pantalla (Rojo = Malo, Verde = Bueno)
        [NotMapped]
        public string ColorEstado => TieneBrechaTecnologica ? "#FF0000" : "#00FF00";
    }
}