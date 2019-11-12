using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pav.Entities;
using System.Data;

namespace pav.DataAcessLayer
{
    public class ProfesorDao
    {
        public IList<Profesor> GetAll()
        {
            List<Profesor> listadoProfesor = new List<Profesor>();

            String strSql = string.Concat(" SELECT idProfe, ",
                                          "        nombre, ",
                                          "        apellido ",
                                          " FROM Profesores",
                                          " WHERE borrado=0 ");

            var resultadoConsulta = DBHelper.GetDBHelper().ConsultaSQL(strSql);

            foreach (DataRow row in resultadoConsulta.Rows)
            {
                listadoProfesor.Add(ObjectMapping(row));
            }

            return listadoProfesor;
        }

        private Profesor ObjectMapping(DataRow row)
        {
            Profesor oProfesor = new Profesor
            {
                IdProfe = Convert.ToInt32(row["idProfe"].ToString()),
                Nombre = row["nombre"].ToString(),
                Apellido = row["apellido"].ToString()
            };

            return oProfesor;
        }

    }
}
