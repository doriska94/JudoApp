using Sempel.Infrastructure;
using Stempel.Domain.Services;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace JudoApp;
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        var b = new ChipFoundNotifeyer();
        b.OnChipFound += B_OnChipFound;
        var a = new GetChipCode(b,new MemberRepository(),new ReciveKeyInput());
        a.Enable();
    }

    private void B_OnChipFound(Stempel.Domain.Model.Member member)
    {
        tet.Text = "Hi " + member.FirstName;
        showMebmerStoryboard.Storyboard.Begin();
    }

    private void Storyboard_Completed(object? sender, EventArgs e)
    {
        hideMebmerStoryboard.Storyboard.Begin();
    }
}
