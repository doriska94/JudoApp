using Stempel.Domain.Model;
using Stempel.Domain.Services;
using Stempel.Infrastructure;
using System.Windows;

namespace JudoApp;
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        var context = new MemberContext();
        var members = context.Members.ToList();
        var b = new ChipFoundNotifeyer();
        b.OnChipFound += OnChipFound;
        var a = new GetChipCode(b, new MemberRepository(context), new ReciveKeyInput());
        a.Enable();
    }

    private void OnChipFound(Member member)
    {
        memberText.Text = ( member.State == State.Arrived? "Hi ":"By By ") + member.FirstName;
        showMebmerStoryboard.Storyboard.Begin();
    }

    private void StoryboardCompleted(object? sender, EventArgs e)
    {
        hideMebmerStoryboard.Storyboard.Begin();
    }
}
