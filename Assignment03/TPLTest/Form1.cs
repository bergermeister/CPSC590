﻿using System;
using System.Net;
using System.Windows.Forms;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TPLTest
{
   public partial class Form1 : Form
   {
      private delegate void ToShow( string aoMsg );

      private bool                    vbTerminate   = false;
      private Thread                  voThrShowTime = null;
      private Task                    voTskShowTime = null;
      private Task                    voTskStkPrice = null;
      private ToShow                  voDelShowTime = null;
      private ToShow                  voDelShowStkP = null;
      private CancellationTokenSource voCancelTkSrc = null;
      private CancellationTokenSource voCancelShowT = null;
      private CancellationTokenSource voCancelShowS = null;

      public Form1( )
      {
         InitializeComponent( );
      }

      private void Form1_Load( object sender, EventArgs e )
      {
         this.voDelShowTime = new ToShow( this.mUpdateTime );
         this.voDelShowStkP = new ToShow( this.mUpdateStockPrice );
      }

      private void voBtnThreadLikeTask_Click( object aoSender, EventArgs aoArgs )
      {
         TcCompute koMC = new TcCompute( );
         Action    koComputeF;
         Thread    koComputeT;

         koMC.VfData1 = 24.22f;
         koMC.ViData2 = 11;
         koComputeF = new Action( koMC.MCompute3 );
         koComputeT = new Thread( new ThreadStart( koComputeF ) );
         koComputeT.Start( );
         koComputeT.Join( );
         MessageBox.Show( "Result = " + koMC.VdRes.ToString( ) );      
      }

      private void voBtnThreadAsTask_Click( object sender, EventArgs e )
      {
         TcCompute koMC       = new TcCompute( );
         Action    koComputeF = new Action( koMC.MCompute3 );
         Task      koTask     = new Task( koComputeF );

         koMC.VfData1 = 24.22f;
         koMC.ViData2 = 11;

         koTask.Start( );
         koTask.Wait( );
         MessageBox.Show( "Result = " + koMC.VdRes.ToString( ) );
      }

      private void voBtnStartThread_Click( object sender, EventArgs e )
      {
         this.vbTerminate = false;

         if( this.voThrShowTime == null )
         {
            this.voThrShowTime = new Thread( new ThreadStart( this.mShowTime ) );
         }
         else if( this.voThrShowTime.ThreadState == ThreadState.Stopped )
         {
            this.voThrShowTime = new Thread( new ThreadStart( this.mShowTime ) );
         }

         this.voThrShowTime.Start( );
      }

      private void voBtnStopThread_Click( object sender, EventArgs e )
      {
         this.vbTerminate = true;
         if( this.voThrShowTime != null )
         {
            if( this.voThrShowTime.ThreadState == ThreadState.Running )
            {
               this.voThrShowTime.Join( );
            }
         }
         this.voLblStatus.Text = "Stopped...";
      }

      private void voBtnStartTask_Click( object sender, EventArgs e )
      {
         this.vbTerminate = false;
         if( this.voTskShowTime == null )
         {
            this.voTskShowTime = new Task( this.mShowTime );
         }
         else if( this.voTskShowTime.IsCompleted )
         {
            this.voTskShowTime = new Task( this.mShowTime );
         }
         this.voTskShowTime.Start( );
      }

      private void voBtnStopTask_Click( object sender, EventArgs e )
      {
         this.vbTerminate = true;
         if( this.voTskShowTime != null )
         {
            if( this.voTskShowTime.Status == TaskStatus.Running )
            {
               this.voTskShowTime.Wait( );
            }
         }
         this.voLblStatus.Text = "Stopped...";
      }

      private void voBtnStartTaskC_Click( object sender, EventArgs e )
      {
         CancellationToken koToken;

         this.voCancelTkSrc = new CancellationTokenSource( );
         koToken = this.voCancelTkSrc.Token;
         if( this.voTskShowTime == null )
         {
            this.voTskShowTime = new Task( ( ) => this.mShowTime( koToken ), koToken );
         }
         else if( this.voTskShowTime.IsCompleted )
         {
            this.voTskShowTime = new Task( ( ) => this.mShowTime( koToken ), koToken );
         }
         this.voTskShowTime.Start( );
      }

      private void voBtnStopTaskC_Click( object sender, EventArgs e )
      {
         try
         {
            this.voCancelTkSrc.Cancel( ); // Cancel tasks
            if( this.voTskShowTime != null )
            {
               if( this.voTskShowTime.Status == TaskStatus.Running )
               {
                  this.voTskShowTime.Wait( );
               }
            }
         }
         catch( AggregateException koEx )
         {
            this.voLblStatus.Text = this.voTskShowTime.Status.ToString( );
            foreach( var koIE in koEx.InnerExceptions )
            {
               MessageBox.Show( koIE.Message );
            }
         }
      }

      private void voBtnStartTwoTasks_Click( object sender, EventArgs e )
      {
         CancellationToken aoToken1;
         CancellationToken aoToken2;

         if( ( this.voTskShowTime == null ) || this.voTskShowTime.IsCompleted )
         {
            this.voCancelTkSrc = new CancellationTokenSource( );
            aoToken1 = this.voCancelTkSrc.Token;
            this.voTskShowTime = new Task( ( ) => this.mShowTime( aoToken1 ), aoToken1 );
         }
         
         if( this.voTskShowTime.Status != TaskStatus.Running )
         {
            this.voTskShowTime.Start( );
         }

         if( ( this.voTskStkPrice == null ) || this.voTskStkPrice.IsCompleted )
         {
            aoToken2 = this.voCancelTkSrc.Token;
            this.voTskStkPrice = new Task( ( ) => this.mShowStockPrice( aoToken2 ), aoToken2 );
         }
          
         if( this.voTskStkPrice.Status != TaskStatus.Running )
         {
            this.voTskStkPrice.Start( );
         }
      }

      private void voBtnStopTwoTasks_Click( object sender, EventArgs e )
      {
         Task koCancel = new Task( this.mCancelAllTasks );
         koCancel.Start( );
      }

      private void voBtnStartSepTasks_Click( object sender, EventArgs e )
      {
         CancellationToken aoToken1;
         CancellationToken aoToken2;
         Action< object >  aoCallback = new Action< object >( this.mShowStockPriceCancelled );

         if( ( this.voTskShowTime == null ) || this.voTskShowTime.IsCompleted )
         {
            this.voCancelShowT = new CancellationTokenSource( );
            aoToken1 = this.voCancelShowT.Token;
            this.voTskShowTime = new Task( ( ) => this.mShowTime( aoToken1 ), aoToken1 );
         }
         
         if( this.voTskShowTime.Status != TaskStatus.Running )
         {
            this.voTskShowTime.Start( );
         }

         if( ( this.voTskStkPrice == null ) || this.voTskStkPrice.IsCompleted )
         {
            this.voCancelShowS = new CancellationTokenSource( );
            aoToken2 = this.voCancelShowS.Token;
            aoToken2.Register( this.mShowStockPriceCancelled, DateTime.Now );
            this.voTskStkPrice = new Task( ( ) => this.mShowStockPrice( aoToken2 ), aoToken2 );
         }
          
         if( this.voTskStkPrice.Status != TaskStatus.Running )
         {
            this.voTskStkPrice.Start( );
         }
      }

      private void voBtnStopTask1_Click( object sender, EventArgs e )
      {
         Task koCancel = new Task( ( ) =>
         {
            try
            {
               this.voCancelShowS.Cancel( );
               this.voTskStkPrice.Wait( );
            }
            catch( AggregateException koEx )
            {
               this.voStatusStrip.Invoke( this.voDelShowStkP, new string[ ]{ this.voTskStkPrice.Status.ToString( ) } );
            }
         } );
         koCancel.Start( );
      }

      private void voBtnStopTask2_Click( object sender, EventArgs e )
      {
         Task koCancel = new Task( ( ) =>
         {
            try
            {
               this.voCancelShowT.Cancel( );
               this.voTskShowTime.Wait( );
            }
            catch( AggregateException koEx )
            {
               this.voStatusStrip.Invoke( this.voDelShowTime, new string[ ]{ this.voTskShowTime.Status.ToString( ) } );
            }
         } );
         koCancel.Start( );
      }

      private void mCancelAllTasks( )
      {
         try
         {
            this.voCancelTkSrc.Cancel( );
            Task.WaitAll( this.voTskShowTime, this.voTskStkPrice );
         }
         catch( AggregateException koEx )
         {
            this.voStatusStrip.Invoke( this.voDelShowTime, new string[ ]{ this.voTskShowTime.Status.ToString( ) } );
            this.voStatusStrip.Invoke( this.voDelShowTime, new string[ ]{ this.voTskStkPrice.Status.ToString( ) } );

            foreach( var koIE in koEx.InnerExceptions )
            {
               MessageBox.Show( koIE.GetType( ) + ":" + koIE.Message );
            }
         }
      }

      private void mShowStockPriceCancelled( object aoState )
      {
         MessageBox.Show( "Show Price Task has been cancelled..." + Environment.NewLine +
                          "Task was started at: " + aoState.ToString( ) );
      }

      private void mShowTime( )
      {
         while( this.vbTerminate == false )
         {
            if( this.voStatusStrip.InvokeRequired )
            {
               if( this.vbTerminate == false )
               {
                  this.voStatusStrip.Invoke( this.voDelShowTime, new string[ ]{ DateTime.Now.ToLongTimeString( ) } );
               }
            }
            Thread.Sleep( 1000 );
         }
      }

      private void mShowTime( CancellationToken aoToken )
      {
         while( true )
         {
            if( this.voStatusStrip.InvokeRequired )
            {
               if( this.vbTerminate == false )
               {
                  this.voStatusStrip.Invoke( this.voDelShowTime, new string[ ]{ DateTime.Now.ToLongTimeString( ) } );
               }
            }
            aoToken.WaitHandle.WaitOne( 1000 );
            aoToken.ThrowIfCancellationRequested( );
         }
      }

      private void mShowStockPrice( CancellationToken aoToken )
      {
         WebClient koClient = new WebClient( );
         byte[ ]   kcpData;
         string    koPageText;
         string    koPrice;
         int       kiPos1;
         int       kiPos2;

         koClient.Headers[ HttpRequestHeader.UserAgent] = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/535.2 (KHTML, like Gecko) Chrome/15.0.874.121 Safari/535.2";

         while( true )
         {
            try
            {
               //kcpData = koClient.DownloadData( "https://www.nasdaq.com/aspx/infoquotes.aspx?symbol=IBM&selected=IBM" );
               //koPageText = new UTF8Encoding( ).GetString( kcpData );
               //kiPos1 = koPageText.IndexOf( "LastSale1'>" );
               //kiPos2 = koPageText.IndexOf( "</", kiPos1 + 1 );
               //koPrice = koPageText.Substring( kiPos1 + 18, kiPos2 - kiPos1 - 18 );
               koPrice = DateTime.Now.Ticks.ToString( );
               if( this.voStatusStrip.InvokeRequired )
               {
                  this.voStatusStrip.Invoke( this.voDelShowStkP, new string[ ]{ koPrice } );
               }
               aoToken.ThrowIfCancellationRequested( );
               aoToken.WaitHandle.WaitOne( 15000 );
            }
            catch( Exception koException )
            {
               throw;
            }
         }
      }

      private void mUpdateTime( string aoMsg )
      {
         this.voLblStatus.Text = aoMsg;
      }

      private void mUpdateStockPrice( string aoMsg )
      {
         this.voLblStockPrice.Text = aoMsg;
      }
   }
}
