﻿namespace GASvcHost
{
   using System;
   using System.ServiceModel;

   class Program
   {
      static void Main( string[ ] koArgs )
      {
         ServiceHost koSH;

         try             
         {                 
            koSH = new ServiceHost( typeof( GASvcLib.Host.Distributor ) );                 
            Console.WriteLine( "Genetic Algorithm Distributor Service Ready, Listening on 7060" );
            Console.WriteLine( "Hit Enter to Stop.." );
            koSH.Open( );                 
            Console.ReadLine( );
         }             
         catch( Exception aoEx )          
         {             
            Console.WriteLine( aoEx.Message );    
         } 
      }
   }
}
