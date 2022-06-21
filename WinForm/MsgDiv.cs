using System;
using System.Windows.Forms;

/// <summary>
/// 消息条回调函数委托
/// </summary>
public delegate void DGMsgDiv();//装方法的容器~~~！

                                /// <summary>
                                /// 消息条类 带Timer计时
                                /// </summary>
public class MsgDiv : System.Windows.Forms.Label
{
    private Timer timerLable = new Timer();

    /// <summary>
    /// 消息回调 委托对象
    /// </summary>
    private DGMsgDiv dgCallBack = null;

    #region 计时器

    /// <summary>
    /// 计时器
    /// </summary>
    public Timer TimerMsg
    {
        get { return timerLable; }
        set { timerLable = value; }
    }

    #endregion 计时器

    #region MsgDiv构造函数

    /// <summary>
    /// MsgDiv构造函数
    /// </summary>
    public MsgDiv()
    {
        InitallMsgDiv(7, 7);
    }

    /// <summary>
    /// MsgDiv构造函数
    /// </summary>
    /// <param name="x">定位x轴坐标</param>
    /// <param name="y">定位y轴坐标</param>
    public MsgDiv(int x, int y)
    {
        InitallMsgDiv(x, y);
    }

    #endregion MsgDiv构造函数

    #region 初始化消息条

    /// <summary>
    /// 初始化消息条
    /// </summary>
    private void InitallMsgDiv(int x, int y)
    {
        this.AutoSize = true;
        this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
        this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        //this.ContextMenuStrip = this.cmsList;
        this.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
        this.ForeColor = System.Drawing.Color.Red;
        this.Location = new System.Drawing.Point(x, y);
        this.MaximumSize = new System.Drawing.Size(980, 525);
        this.Name = "msgDIV";
        this.Padding = new System.Windows.Forms.Padding(7);
        this.Size = new System.Drawing.Size(71, 31);
        this.TabIndex = 1;
        this.Text = "消息条";
        this.Visible = false;
        //给委托添加事件
        this.DoubleClick += new System.EventHandler(this.msgDIV_DoubleClick);
        this.MouseLeave += new System.EventHandler(this.msgDIV_MouseLeave);
        this.MouseHover += new System.EventHandler(this.msgDIV_MouseHover);
        this.timerLable.Interval = 1000;
        this.timerLable.Tick += new System.EventHandler(this.timerLable_Tick);
    }

    #endregion 初始化消息条

    #region 将消息条添加到指定容器上

    /// <summary>
    /// 将消息条添加到指定容器上Form
    /// </summary>
    /// <param name="form"></param>
    public void AddToControl(Form form)
    {
        form.Controls.Add(this);
    }

    /// <summary>
    /// 将消息条添加到指定容器上GroupBox
    /// </summary>
    /// <param name="form"></param>
    public void AddToControl(GroupBox groupBox)
    {
        groupBox.Controls.Add(this);
    }

    /// <summary>
    /// 将消息条添加到指定容器上Panel
    /// </summary>
    /// <param name="form"></param>
    public void AddToControl(Panel panel)
    {
        panel.Controls.Add(this);
    }

    #endregion 将消息条添加到指定容器上

    //---------------------------------------------------------------------------

    #region 消息显示 的相关参数们 hiddenClick，countNumber，constCountNumber

    /// <summary>
    /// 当前显示了多久的秒钟数
    /// </summary>
    private int hiddenClick = 0;

    /// <summary>
    /// 要显示多久的秒钟数 可变参数
    /// </summary>
    private int countNumber = 3;

    /// <summary>
    /// 要显示多久的秒钟数 固定参数
    /// </summary>
    private int constCountNumber = 3;

    #endregion 消息显示 的相关参数们 hiddenClick，countNumber，constCountNumber

    #region 计时器 显示countNumber秒钟后自动隐藏div -timerLable_Tick(object sender, EventArgs e)

    private void timerLable_Tick(object sender, EventArgs e)
    {
        if (hiddenClick > countNumber - 2)
        {
            MsgDivHidden();
        }
        else
        {
            hiddenClick++;
            //RemainCount();
        }
    }

    #endregion 计时器 显示countNumber秒钟后自动隐藏div -timerLable_Tick(object sender, EventArgs e)

    #region 隐藏消息框 并停止计时 +void MsgDivHidden()

    /// <summary>
    /// 隐藏消息框 并停止计时
    /// </summary>
    public void MsgDivHidden()
    {
        this.Text = "";
        this.Visible = false;
        this.hiddenClick = 0;
        //this.tslblRemainSecond.Text = "";
        if (this.timerLable.Enabled == true)
            this.timerLable.Stop();

        //调用 委托 然后清空委托
        if (dgCallBack != null && dgCallBack.GetInvocationList().Length > 0)
        {
            dgCallBack();
            dgCallBack -= dgCallBack;
        }
    }

    #endregion 隐藏消息框 并停止计时 +void MsgDivHidden()

    #region 在消息框中显示消息字符串 +void MsgDivShow(string msg)

    /// <summary>
    /// 在消息框中显示消息字符串
    /// </summary>
    /// <param name="msg">要显示的字符串</param>
    public void MsgDivShow(string msg)
    {
        this.Text = msg;
        this.Visible = true;
        this.countNumber = constCountNumber;//默认设置显示秒数为10;
        this.hiddenClick = 0;//重置倒数描述
        this.timerLable.Start();
    }

    #endregion 在消息框中显示消息字符串 +void MsgDivShow(string msg)

    #region 在消息框中显示消息字符串 并在消息消失时 调用回调函数 +void MsgDivShow(string msg, DGMsgDiv callback)

    /// <summary>
    /// 在消息框中显示消息字符串 并在消息消失时 调用回调函数
    /// </summary>
    /// <param name="msg">要显示的字符串</param>
    /// <param name="callback">回调函数</param>
    public void MsgDivShow(string msg, DGMsgDiv callback)
    {
        MsgDivShow(msg);
        dgCallBack = callback;
    }

    #endregion 在消息框中显示消息字符串 并在消息消失时 调用回调函数 +void MsgDivShow(string msg, DGMsgDiv callback)

    #region 在消息框中显示消息字符串 并在指定时间消息消失时 调用回调函数 +void MsgDivShow(string msg, int seconds, DGMsgDiv callback)

    /// <summary>
    /// 在消息框中显示消息字符串 并在消息消失时 调用回调函数
    /// </summary>
    /// <param name="msg">要显示的字符串</param>
    /// <param name="seconds">消息显示时间</param>
    /// <param name="callback">回调函数</param>
    public void MsgDivShow(string msg, int seconds, DGMsgDiv callback)
    {
        MsgDivShow(msg, seconds);
        dgCallBack = callback;
    }

    #endregion 在消息框中显示消息字符串 并在指定时间消息消失时 调用回调函数 +void MsgDivShow(string msg, int seconds, DGMsgDiv callback)

    #region 在消息框中显示消息字符串，并指定消息框显示秒数 +void MsgDivShow(string msg, int seconds)

    /// <summary>
    /// 在消息框中显示消息字符串，并指定消息框显示秒数
    /// </summary>
    /// <param name="msg">要显示的字符串</param>
    /// <param name="seconds">消息框显示秒数</param>
    public void MsgDivShow(string msg, int seconds)
    {
        this.Text = msg;
        this.Visible = true;
        this.countNumber = seconds;
        this.hiddenClick = 0;//重置倒数描述
        this.timerLable.Start();
    }

    #endregion 在消息框中显示消息字符串，并指定消息框显示秒数 +void MsgDivShow(string msg, int seconds)

    //---------------------------------------------------------------------------

    #region 事件们～～～！ msgDIV_MouseHover，msgDIV_MouseLeave，msgDIV_DoubleClick

    //当鼠标停留在div上时 停止计时
    private void msgDIV_MouseHover(object sender, EventArgs e)
    {
        if (this.timerLable.Enabled == true)
            this.timerLable.Stop();
    }

    //当鼠标从div上移开时 继续及时
    private void msgDIV_MouseLeave(object sender, EventArgs e)
    {
        //当消息框正在显示、回复框没显示、计时器正停止的时候，重新启动计时器
        if (this.Visible == true && this.timerLable.Enabled == false)
            this.timerLable.Start();
    }

    //双击消息框时关闭消息框
    private void msgDIV_DoubleClick(object sender, EventArgs e)
    {
        MsgDivHidden();
    }

    #endregion 事件们～～～！ msgDIV_MouseHover，msgDIV_MouseLeave，msgDIV_DoubleClick
}