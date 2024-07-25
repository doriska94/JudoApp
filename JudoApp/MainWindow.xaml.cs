using JudoApp.Services;
using Stempel.Domain.Model;
using Stempel.Domain.Services;
using Stempel.Infrastructure;
using System.Windows;

namespace JudoApp;
public partial class MainWindow : Window
{
    private StampTimeRepository _stampTimeRepository;
    private readonly ChipFoundNotifyer _notifyer;
    private readonly StampContext _context;
    private readonly RFIDChipCode _RFIDService;

    public MainWindow()
    {
        InitializeComponent();

        _context = new StampContext(Configurations.GetDefaultConectionString());

        _stampTimeRepository = new StampTimeRepository(_context);
        var memberRepository = new MemberRepository(_context);

        _notifyer = new ChipFoundNotifyer();
        _notifyer.OnChipFound += OnChipFound;

        _RFIDService = new RFIDChipCode(_notifyer, memberRepository, new ReciveKeyInput());
        _RFIDService.Enable();
    }

    private void OnChipFound(Member member)
    {
        _stampTimeRepository.AddNow(member);

        memberText.Text = (member.LastState == StampState.Arrived ? "Hi " : "Bye Bye ") + member.FirstName;
        showMemberStoryboard.Storyboard.Begin();
    }

    private void StoryboardCompleted(object? sender, EventArgs e)
    {
        hideMemberStoryboard.Storyboard.Begin();
    }

    private void OnWindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
    {
        _RFIDService.Disable();
        _notifyer.OnChipFound -= OnChipFound;
        _context.Dispose();
    }
}
