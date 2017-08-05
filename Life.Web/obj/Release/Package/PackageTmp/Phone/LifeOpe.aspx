<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LifeOpe.aspx.cs" StylesheetTheme="Detail"
    Inherits="Life.Web.Phone.LifeOpe" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0" />
    <title></title>
    <script src="/Content/Script/Jquery/jquery-1.8.2.min.js" type="text/javascript"></script>
    <link href="/Content/Script/mobiscroll/css/jquery.scroller-1.0.2.min.css" rel="stylesheet"
        type="text/css" />
    <script src="/Content/Script/mobiscroll/js/jquery.scroller-1.0.2.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#txtReason').scroller();
        });
        
        function checkSub() {
            var sNull = /^\s+$/;
            var reson = document.getElementById("txtReason");
            if (reson.value == "" || sNull.test(reson.value)) {
                alert("请输入消费名称.");
                reson.focus();
                return false;
            }
            var price = document.getElementById("txtPrice");
            if (price.value == "" || sNull.test(price.value)) {
                alert("请输入消费金额.");
                price.focus();
                return false;
            }
            var reg = /^\d+(\.(\d)+)?$/;
            if (!reg.test(price.value)) {
                alert("消费金额格式不正确.");
                price.select();
                return false;
            }
            if (document.getElementById("txtDate").value == "") {
                alert('消费时间不能为空.');
                document.getElementById("txtDate").focus();
                return false
            }
            return true;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="div_top">
        <table class="detail" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <th colspan="2">
                    <asp:Label ID="labTitle" runat="server" Text="增加生活费信息"></asp:Label>
                </th>
            </tr>
            <tr class="tr_1">
                <td width="80">
                    消费名称
                </td>
                <td>
                    <asp:TextBox ID="txtReason" runat="server" CssClass="txt" Width="220px"></asp:TextBox>
                </td>
            </tr>
            <tr class="tr_2">
                <td>
                    消费金额
                </td>
                <td>
                    <asp:TextBox ID="txtPrice" runat="server" CssClass="txt" Width="220px"></asp:TextBox>
                </td>
            </tr>
            <tr class="tr_1">
                <td>
                    消费时间
                </td>
                <td>
                    <asp:TextBox ID="txtDate" CssClass="txt" runat="server" Width="220px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    消费类型
                </td>
                <td>
                    <asp:DropDownList ID="ddlCostType" runat="server" CssClass="txt" Width="225px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr class="tr_2">
                <td>
                    备&nbsp;&nbsp;注
                </td>
                <td>
                    <asp:TextBox ID="txtNotes" TextMode="MultiLine" runat="server" CssClass="txt" Height="50px"
                        Width="220px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Button ID="btnSave" runat="server" Text="保存" OnClientClick="return checkSub()" />&nbsp;
                    <input type="button" value="取消" onclick="closeWindow()" />
                </td>
            </tr>
        </table>
    </div>
    </form>
    <script type="text/javascript">
        $(function () {
            //初始化日期控件
            var opt = {
                preset: 'date', //日期
                theme: 'ios', //皮肤样式
                display: 'modal', //显示方式 
                mode: 'clickpick', //日期选择模式
                dateFormat: 'yy-mm-dd', // 日期格式
                setText: '确定', //确认按钮名称
                cancelText: '取消', //取消按钮名籍我
                dateOrder: 'yymmdd', //面板中日期排列格式
                dayText: '日', monthText: '月', yearText: '年', //面板中年月日文字
                endYear: 2020 //结束年份
            };

            $('#txtDate').scroller();
        });
    </script>
</body>
</html>
