﻿using System.Collections.Generic;

namespace _RTSGameProject.Logic.Common.SaveLoad
{
    public interface IKeyProvider
    {
        string Provide<TData>();
        IEnumerable<string> ProvideAll();
    }
}