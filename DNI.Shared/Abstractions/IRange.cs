﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Shared.Abstractions
{
    public interface IRange<T>: IEquatable<T>
    {
        bool Equals(IRange<T> value);
        bool IsInRange(T value);
        T Start { get; }
        T End { get; }
    }
}