using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pav.DataAcessLayer;
using pav.Entities;


namespace pav.BusinessLayer
{
    public class ProfesorService
    {
        private ProfesorDao oProfesorDao;
        public ProfesorService()
        {
            oProfesorDao = new ProfesorDao();
        }
        public IList<Profesor> ObtenerTodos()
        {
            return oProfesorDao.GetAll();
        }
    }
}
