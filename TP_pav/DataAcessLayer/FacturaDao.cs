using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pav.Entities;

namespace pav.DataAcessLayer
{
    class FacturaDao
    {
        internal bool Create(Factura factura)
        {
            DataManager dm = new DataManager();
            try
            {
                dm.Open();
                dm.BeginTransaction();

                string sql = string.Concat("INSERT INTO [dbo].[Facturas] ",
                                            // "           ([fecha]         ",
                                            "           ([idCliente]       ",
                                            "           ,[idTipo]   ",
                                            "           ,[subtotal]    ",
                                            "           ,[descuento]) ",
                                            "     VALUES                 ",
                                            //    "           (@fecha          ",
                                            "(", factura.Cliente.IdCliente,
                                            " , ", factura.TipoFactura.IdTipoFactura,
                                            " , ", factura.SubTotal,
                                            " , ", factura.Descuento, " ) ");


                // comento los "numero" por q en la tabla estan como identity

                /*
                var parametros = new Dictionary<string, object>();
               // parametros.Add("numero", factura.NroFactura);
               // parametros.Add("fecha", factura.Fecha);
                parametros.Add("idCliente", factura.Cliente.IdCliente);
                parametros.Add("idTipo", factura.TipoFactura.IdTipoFactura);
                parametros.Add("subtotal", factura.SubTotal);
                parametros.Add("descuento", factura.Descuento);
                dm.EjecutarSQLCONPARAMETROS(sql, parametros);
                */

                dm.EjecutarSQL(sql);

                var newId = dm.ConsultaSQLScalar(" SELECT @@IDENTITY");
                factura.NroFactura = Convert.ToInt32(newId);


                foreach (var itemFactura in factura.FacturaDetalle)
                {
                    string sqlDetalle = string.Concat(" INSERT INTO [dbo].[DetalleF] ",
                                                        "           ([numeroFactura]           ",
                                                        "           ,[idArticulo]          ",
                                                        "           ,[precioUnitario]      ",
                                                        "           ,[cantidad])             ",
                                                        "     VALUES                        ",
                                                        "(", factura.NroFactura,
                                                        " , ", itemFactura.IdArticulo,
                                                        " , ", itemFactura.PrecioUnitario,
                                                        " , ", itemFactura.Cantidad, ")");

                    /*
                    var paramDetalle = new Dictionary<string, object>();
                    //paramDetalle.Add("id_factura", factura.IdFactura);
                    paramDetalle.Add("id_producto", itemFactura.IdArticulo);
                    paramDetalle.Add("precio_unitario", itemFactura.PrecioUnitario);
                    paramDetalle.Add("cantidad", itemFactura.Cantidad);
                    */

                    dm.EjecutarSQL(sqlDetalle);
                }



                dm.Commit();

            }
            catch (Exception ex)
            {
                dm.Rollback();
                throw ex;
            }
            finally
            {
                // Cierra la conexión 
                dm.Close();
            }
            return true;
        }
    }
}
