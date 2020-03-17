using Mvc_Maneiro.Models;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mvc_Maneiro.Services
{
    public class AlunoService
    {

        public List<Aluno> GetAlunos()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                return session.Query<Aluno>().OrderBy(x => x.Nome.Trim()).ToList();
            }
        }
        public List<Aluno> GetAlunosByDescription(string descricao)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                return session.Query<Aluno>().Where(x =>
                       x.Nome.ToUpper().Contains(descricao.ToUpper()) ||
                       x.Curso.ToUpper().Contains(descricao.ToUpper()) ||
                       x.Email.ToUpper().Contains(descricao.ToUpper()) ||
                       x.Sexo.ToUpper().Contains(descricao.ToUpper())
                        ).ToList();
            }
        }
        public Aluno GetAlunoById(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                return session.Get<Aluno>(id);
            }
        }

        public void CriarAluno(Aluno aluno)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(aluno);
                    transaction.Commit();
                }
            }
        }
        public void EditarAluno(int id, Aluno aluno)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var alunoAlterado = session.Get<Aluno>(id);
                alunoAlterado.Sexo = aluno.Sexo;
                alunoAlterado.Curso = aluno.Curso;
                alunoAlterado.Email = aluno.Email;
                alunoAlterado.Nome = aluno.Nome;

                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(alunoAlterado);
                    transaction.Commit();
                }
            }
        }
        public void DeletarAluno(Aluno aluno)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Delete(aluno);
                    transaction.Commit();
                }
            }
        }

    }
}