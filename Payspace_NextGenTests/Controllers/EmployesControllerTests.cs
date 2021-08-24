using NUnit.Framework;
using Payspace_NextGen.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payspace_NextGen.Controllers.Tests
{
    [TestFixture()]
    public class EmployesControllerTests
    {
        public List<Models.Employe> lista;
        APIEmployesController aPIEmployes;
        [SetUp]
        public void _setup()
        {

        }
        [Test()]
        public void CreateTest()
        {
            aPIEmployes = new APIEmployesController();
            foreach (Models.Employe item in aPIEmployes.GetEmployes())
            {
                lista.Add(item);
            }
            Assert.Pass("Retornou o Empregado Cadastrado");
        }
    }
}