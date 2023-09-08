# IEL Estudantes

This project is a test answer requested by IEL made with C#.
Deadline: 2 days.
Check [Functionalities](#functionalities) to see what was implemented

## Software Description

```
 "Desenvolver uma aplicação web ASP.NET, C#, MVC, responsável em manter o cadastro de estudantes do IEL. Essa aplicação deve conter uma página de login de acesso, onde o usuário logado visualizará uma página com uma listagem dos estudantes cadastrados e um campo de pesquisa. Nela, também, deverão estar visíveis um botão “Buscar”, para filtrar a listagem pelo nome do estudante, e um botão de “Novo”, que direcionará para a tela de cadastro do estudante.

O estudante deverá ter os seguintes atributos:
	Nome (texto livre com 100 caracteres);
	CPF (tem que validar o formato e inserir a máscara);
	Endereço (texto livre com 200 caracteres)
	Cidade (Lista de cidade carregada a partir do Estado)
	Estado (Lista com as UFs);
	Instituição de Ensino (Lista)
	Nome do curso (texto livre com 100 caracteres);
	Data de conclusão (data)
Todos os campos são obrigatórios.
Na listagem dos estudantes deve permitir excluir um estudante, exibindo uma janela de confirmação.
OBSERVAÇÕES:
O Teste terá uma duração de 48 horas para a sua execução.
A utilização de frameworks e componentes (JQuery, Bootstrap etc.) para execução do teste será um diferencial na avaliação.
A solução deverá ser disponibilizada no GitHub (https://github.com/) em um repositório público e enviado o link para o e-mail do aplicador do teste.
"
```

## 🚀 Getting Started

These instructions will help you get a copy of the project up and running on your local machine for development and testing purposes.

See [Functionalities](#deployment) for notes on the deployed project.

### 📋 Prerequisites

```
Visual Studio Professional
Git/Git Bash
```

### 🔧 Installation

To install the project, follow these steps:

1. Clone the repository using the command `git clone`.

[GithubUsers](https://github.com/caiocesar333/GithubUsers) - private repository

2. After cloning the repository, open it in Visual Studio.

## ✒️ Authors

[Caio César](https://github.com/caiocesar333)

## 📄 License

This project is a challenge and is not licensed. See the [LICENSE.md](https://github.com/caiocesar333/project/license) file for details.

## <a id="functionalities"></a>🛠️ Functionalities / Steps

### Studants CRUD

```
namespace IEL.Controllers
{
    public class EstudanteController : Controller
    {
        private readonly EstudantesDbContext _context;

        public EstudanteController(EstudantesDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var estudantes = await _context.Estudantes.ToListAsync();
            if (estudantes == null || !estudantes.Any())
            {
                Console.WriteLine("error");
            }
            return View(estudantes);
        }

        [HttpPost]
        public async Task<IActionResult> Index(string searchText) // Search method
        {
            if (string.IsNullOrEmpty(searchText))
            {
                return View(await _context.Estudantes.ToListAsync());
            }

            var filteredEstudantes = await _context.Estudantes
                .Where(e => e.Nome.Contains(searchText))
                .ToListAsync();
            return PartialView("_EstudanteList", filteredEstudantes);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Estados = Enum.GetValues(typeof(EstadoEn));
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Estudante estudante)
        {
            if (ModelState.IsValid)
            {
                // Assume Estado is an enum and you've adjusted your database accordingly.
                // estudante.Estado will now hold the integer representation of the Estado enum value

                _context.Add(estudante);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // If we reach here, something went wrong
            ViewBag.Estados = Enum.GetValues(typeof(EstadoEn));
            return View(estudante);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Console.WriteLine($"Attempting to delete estudante with id: {id}");

            var estudante = await _context.Estudantes.FindAsync(id);
            if (estudante == null)
            {
                Console.WriteLine($"Estudante with id: {id} not found.");
                return NotFound();
            }

            _context.Estudantes.Remove(estudante);
            await _context.SaveChangesAsync();

            Console.WriteLine($"Successfully deleted estudante with id: {id}");

            return RedirectToAction(nameof(Index));
        }

    }
}
```

### User login with Identity
```
namespace IEL.Controllers
{
    public class LoginController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public LoginController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

                if (model.Email == "caio@1234" && model.Password == "1234")
                {
                    return RedirectToAction("Index", "Estudante");
                }

                // If we reach this point, login failed.
                TempData["Error"] = "Invalid login attempt.";
            }

            return View(model);
        }

    }
}

```
Note that the FrontEnd is on a Static site.