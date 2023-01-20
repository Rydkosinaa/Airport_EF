using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Airport.Models;
using static System.Collections.Specialized.BitVector32;

namespace Airport
{
    public class MultiThreading
    {
        private AutoResetEvent waitHandler = new(true);
        private Mutex mutex = new();
        private Semaphore semaphore = new(0, 1);
        private DbContextOptions options;
        public MultiThreading(DbContextOptions options)
        {
            this.options = options;
        }

        public void LockExample()
        {
            for (int i = 0; i < 40; i++)
            {
                using (AirportContext context = new(options))
                {
                    Thread myThread = new(() =>
                    {
                        object? locker = new object();
                        lock (locker!)
                        {
                            context.Airlines.AddAsync(new Airline { AirlineName = "Airline " + i, Plane_quont =  i, Route_quont=i });
                            context.SaveChanges();
                        }
                    });
                    myThread.Start();
                }
            }
        }

        public void MonitorExample()
        {
            object? locker = new();

            for (int i = 41; i < 80; i++)
            {
                using (AirportContext context = new(options))
                {
                    Thread myThread = new(() =>
                    {
                        bool acquiredLock = false;
                        try
                        {
                            Monitor.Enter(locker, ref acquiredLock);

                            context.Airlines.AddAsync(new Airline { AirlineName = "Airline " + i, Plane_quont = i, Route_quont = i });
                            context.SaveChanges();
                        }
                        finally
                        {
                            if (acquiredLock)
                            {
                                Monitor.Exit(locker);
                            }
                        }
                    });
                    myThread.Start();
                }

            }
        }

        public void AutoResetEventExample()
        {
            for (int i = 81; i < 110; i++)
            {
                using (AirportContext context = new(options))
                {
                    Thread myThread = new(() =>
                    {
                        waitHandler.WaitOne();
                        context.Airlines.AddAsync(new Airline { AirlineName = "Airline " + i, Plane_quont = i, Route_quont = i });

                        context.SaveChanges();
                        waitHandler.Set();
                    });
                    myThread.Start();
                }

            }
        }

        public void MutexExample()
        {
            for (int i = 111; i < 150; i++)
            {
                using (AirportContext context = new(options))
                {
                    Thread myThread = new(() =>
                    {
                        mutex.WaitOne();
                        context.Airlines.AddAsync(new Airline { AirlineName = "Airline " + i, Plane_quont = i, Route_quont = i });
                        context.SaveChanges();
                        mutex.ReleaseMutex();
                    });
                    myThread.Start();
                }

            }
        }

        public void SemaphoreExample()
        {
            for (int i = 151; i < 300; i++)
            {
                using (AirportContext context = new(options))
                {
                    Thread myThread = new(() =>
                    {
                        semaphore.WaitOne();
                        context.Airlines.AddAsync(new Airline { AirlineName = "Airline " + i, Plane_quont = i, Route_quont = i });
                        context.SaveChanges();
                        semaphore.Release();
                    });
                    myThread.Start();
                }
            }
        }
    }
}