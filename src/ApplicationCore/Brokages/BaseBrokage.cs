using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Brokages
{
    public abstract class BaseBrokage
    {
        private readonly BrokageSettings _settings;
        private ConnectionStatus _connectionStatus = ConnectionStatus.DISCONNECTED;
        public BaseBrokage(BrokageName name, BrokageSettings settings)
        {
            _settings = settings;
            Name = name;
        }

        public BrokageName Name { get; private set; }

        protected BrokageSettings BrokageSettings => _settings;

        public ConnectionStatus ConnectionStatus => _connectionStatus;
        public virtual bool Connectted => false;
        protected void SetConnectionStatus(ConnectionStatus status)
        {
            if (status != _connectionStatus)
            {
                _connectionStatus = status;
                OnConnectionStatusChanged(status);
            }
        }

        public event EventHandler ConnectionStatusChanged;
        void OnConnectionStatusChanged(ConnectionStatus status)
        {
            ConnectionStatusChanged?.Invoke(this, new ConnectionStatusEventArgs(status));
        }

        private bool _ready = false;
        protected void SetReady(bool val)
        {
            if (val) OnReady();
            _ready = val;
        }

        public event EventHandler Ready;
        protected void OnReady()
        {
            Ready?.Invoke(this, new EventArgs());
        }

        public event EventHandler ExceptionHappend;
        protected void OnExceptionHappend(Exception ex)
        {
            ExceptionHappend?.Invoke(this, new ExceptionEventArgs(ex));
        }

        public event EventHandler ActionExecuted;
        protected void OnActionExecuted(ActionEventArgs e) => ActionExecuted?.Invoke(this, e);

        public abstract void Connect();

        public abstract void DisConnect();

        public abstract void RequestAccountPositions(string account, string symbol = "");

        public abstract void MakeOrder(string symbol, string account, decimal price, int lots, bool dayTrade);

        public abstract string ClearOrders(string symbol, string account);

    }
}
