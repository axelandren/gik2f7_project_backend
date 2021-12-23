using System;

namespace ProjektWebApi.Database
{
    public interface IDatabaseBootstrap
    {
        // Used in Startup file to access database
        void Setup();
    }
}