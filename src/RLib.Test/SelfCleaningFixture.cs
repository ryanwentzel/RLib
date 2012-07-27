using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;

namespace RLib
{
    [TestFixture]
    public abstract class SelfCleaningFixture : IDisposable
    {
        private bool _disposed;
        private FileSystemWatcher _watcher;

        private List<string> _generatedFiles;

        [TestFixtureSetUp]
        public void BaseFixtureSetup()
        {
            _generatedFiles = new List<string>();
            _watcher = new FileSystemWatcher(".")
            {
                Filter = "*.*",
                NotifyFilter =
                    NotifyFilters.LastAccess 
                    | NotifyFilters.LastWrite 
                    | NotifyFilters.FileName
                    | NotifyFilters.DirectoryName
            };
            _watcher.Created += OnFileCreated;
            _watcher.EnableRaisingEvents = true;
        }

        [TestFixtureTearDown]
        public void BaseFixtureTeardown()
        {
            Dispose();
        }

        void OnFileCreated(object sender, FileSystemEventArgs e)
        {
            _generatedFiles.Add(e.FullPath);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                if (_watcher != null)
                {
                    _watcher.EnableRaisingEvents = false;
                    _watcher.Created -= OnFileCreated;
                    _watcher.Dispose();

                    _generatedFiles.ForEach(path =>
                    {
                        try
                        {
                            File.Delete(path);
                        }
                        catch (Exception)
                        {
                        }
                    });
                }
            }

            _disposed = true;
        }
    }
}
