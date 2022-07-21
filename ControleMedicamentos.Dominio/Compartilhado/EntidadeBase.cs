using System;
using Taikandi;

namespace ControleMedicamentos.Dominio
{
    public abstract class EntidadeBase<T>
    {
        public Guid Id { get; set; }

        public EntidadeBase()
        {
            Id = SequentialGuid.NewGuid();
        }
    }
}
