//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ErpDataAccessFromSystime.Erps.DmsV1.DataAcces
{
    using System;
    using System.Collections.Generic;
    
    public partial class referencias
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public referencias()
        {
            this.tall_citas = new HashSet<tall_citas>();
            this.tall_encabeza_orden = new HashSet<tall_encabeza_orden>();
        }
    
        public string codigo { get; set; }
        public string descripcion { get; set; }
        public string codigo_oferta { get; set; }
        public string generico { get; set; }
        public string clase { get; set; }
        public byte contable { get; set; }
        public string grupo { get; set; }
        public string subgrupo { get; set; }
        public decimal nit { get; set; }
        public Nullable<decimal> valor_unitario { get; set; }
        public Nullable<float> porcentaje_iva { get; set; }
        public Nullable<decimal> costo_unitario { get; set; }
        public bool maneja_inventario { get; set; }
        public string und_1 { get; set; }
        public Nullable<float> can_1 { get; set; }
        public string und_2 { get; set; }
        public Nullable<float> can_2 { get; set; }
        public string und_3 { get; set; }
        public Nullable<float> can_3 { get; set; }
        public Nullable<byte> und_vta { get; set; }
        public Nullable<byte> und_com { get; set; }
        public Nullable<float> impoconsumo { get; set; }
        public Nullable<decimal> valor_und1 { get; set; }
        public Nullable<decimal> valor_und2 { get; set; }
        public Nullable<decimal> valor_und3 { get; set; }
        public Nullable<float> conversion { get; set; }
        public Nullable<float> otro_impuesto { get; set; }
        public Nullable<float> minimo_iva { get; set; }
        public Nullable<decimal> minimo_iva_c { get; set; }
        public string precio_marcado { get; set; }
        public Nullable<float> factor_compra { get; set; }
        public Nullable<float> factor_venta_1 { get; set; }
        public Nullable<float> factor_venta_2 { get; set; }
        public Nullable<float> factor_venta_3 { get; set; }
        public Nullable<float> factor_venta_4 { get; set; }
        public Nullable<float> factor_venta_5 { get; set; }
        public Nullable<float> factor_venta_6 { get; set; }
        public Nullable<float> factor_venta_7 { get; set; }
        public Nullable<float> factor_venta_8 { get; set; }
        public Nullable<float> factor_venta_9 { get; set; }
        public Nullable<float> factor_venta_10 { get; set; }
        public Nullable<System.DateTime> fec_cambio_precio { get; set; }
        public Nullable<decimal> costo_anterior { get; set; }
        public Nullable<System.DateTime> fec_ultima_entrada { get; set; }
        public Nullable<System.DateTime> fec_ultima_salida { get; set; }
        public Nullable<float> impoconsumo_compra { get; set; }
        public Nullable<float> porcentaje_iva_compra { get; set; }
        public Nullable<decimal> precio_si_costo_cero { get; set; }
        public Nullable<System.DateTime> fecha_creacion { get; set; }
        public Nullable<float> maximo_descuento { get; set; }
        public string maneja_lotes { get; set; }
        public string maneja_otra_und { get; set; }
        public string otra_und { get; set; }
        public Nullable<float> tam_alto { get; set; }
        public Nullable<float> tam_largo { get; set; }
        public Nullable<float> tam_ancho { get; set; }
        public Nullable<decimal> costo_estandar { get; set; }
        public Nullable<float> reposicion { get; set; }
        public string ref_anulada { get; set; }
        public Nullable<System.DateTime> vcmto_refer { get; set; }
        public Nullable<float> impodeporte { get; set; }
        public string subgrupo2 { get; set; }
        public string subgrupo3 { get; set; }
        public string controlado { get; set; }
        public string promocion { get; set; }
        public string maneja_series { get; set; }
        public string codigo_enlace { get; set; }
        public Nullable<float> cantidad_enlace { get; set; }
        public string usar_descto_cliente { get; set; }
        public string usar_dcto_vol { get; set; }
        public Nullable<decimal> costo_compra_emergencia { get; set; }
        public string codigo_descuento { get; set; }
        public string pedir { get; set; }
        public Nullable<int> tipo_1 { get; set; }
        public Nullable<int> tipo_2 { get; set; }
        public Nullable<int> tipo_3 { get; set; }
        public Nullable<int> tipo_4 { get; set; }
        public Nullable<int> tipo_5 { get; set; }
        public Nullable<int> tipo_6 { get; set; }
        public Nullable<int> tipo_7 { get; set; }
        public string iva_es_costo { get; set; }
        public string calificacion_abc { get; set; }
        public string grupo_comision { get; set; }
        public int id { get; set; }
        public Nullable<float> cantidad_promocion_max { get; set; }
        public Nullable<System.DateTime> fecha_inicia_promocion { get; set; }
        public Nullable<decimal> precio_pos { get; set; }
        public Nullable<System.DateTime> fecha_actualizacion { get; set; }
        public Nullable<bool> borrar_promocion { get; set; }
        public Nullable<float> porcentaje_impoconsumo { get; set; }
        public string no_aplica_retenciones { get; set; }
        public string accesorio { get; set; }
        public string Notas { get; set; }
        public Nullable<int> tipo_8 { get; set; }
        public Nullable<int> tipo_9 { get; set; }
        public string restringir_pos { get; set; }
        public Nullable<bool> esLubricante { get; set; }
        public Nullable<bool> EsPintura { get; set; }
        public Nullable<bool> esNeumatico { get; set; }
        public Nullable<int> MinDespacho { get; set; }
        public string codDIV { get; set; }
        public Nullable<bool> si_flota { get; set; }
        public string Debajo_Costo { get; set; }
        public Nullable<float> peso1 { get; set; }
        public Nullable<float> peso2 { get; set; }
        public string proveedor_unico { get; set; }
    
        public virtual referencias_imp referencias_imp { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tall_citas> tall_citas { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tall_encabeza_orden> tall_encabeza_orden { get; set; }
        public virtual terceros terceros { get; set; }
    }
}
