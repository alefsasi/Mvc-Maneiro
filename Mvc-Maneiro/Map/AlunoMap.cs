using FluentNHibernate.Mapping;
using Mvc_Maneiro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mvc_Maneiro.Map
{
    public class AlunoMap : ClassMap<Aluno>
    {
        public AlunoMap()
        {
            Id(x => x.Id);
            Map(x => x.Nome).Not.Nullable();
            Map(x => x.Email).Not.Nullable();
            Map(x => x.Curso).Not.Nullable();
            Map(x => x.Sexo).Not.Nullable();
            Table("Alunos");
        }
    }
}