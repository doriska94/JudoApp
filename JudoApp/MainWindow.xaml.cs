using JudoApp.Services;
using Stempel.Domain.Model;
using Stempel.Domain.Repositories;
using Stempel.Domain.Services;
using Stempel.Infrastructure;
using System.Configuration;
using System.Windows;

namespace JudoApp;
public partial class MainWindow : Window
{
    private StampTimeRepository _stampTimeRepository;
    public MainWindow()
    {
        InitializeComponent();
        var context = new StampContext(Configurations.GetDefaultConectionString());
        _stampTimeRepository = new StampTimeRepository(context);

        var _stampRepository = _stampTimeRepository;
        var memberRepository = new MemberRepository(context);

        var b = new ChipFoundNotifeyer();
        b.OnChipFound += OnChipFound;
        
        var a = new RFIDChipCode(b, memberRepository, new ReciveKeyInput());
        a.Enable();
    }

    private void OnChipFound(Member member)
    {
        _stampTimeRepository.AddNow(member);

        memberText.Text = ( member.LastState == StampState.Arrived? "Hi ":"By By ") + member.FirstName;
        showMebmerStoryboard.Storyboard.Begin();
    }

    private void StoryboardCompleted(object? sender, EventArgs e)
    {
        hideMebmerStoryboard.Storyboard.Begin();
    }
}
