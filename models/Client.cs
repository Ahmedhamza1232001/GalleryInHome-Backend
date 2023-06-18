using Proj.Api.models;

namespace Proj;
public class Client:User
{

    public List<Favorite>? Favorites { get; set; }
    public List<Card>? Card { get; set; }
}
