using System.Data.Common;
using System.Data.Entity.Infrastructure;

namespace MR.AspNet.Identity.EntityFramework6
{
	/// <summary>
	/// Base class for the Entity Framework database context used for identity.
	/// </summary>
	public class IdentityDbContextInt : IdentityDbContextInt<IdentityUserInt>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="IdentityDbContext" /> class using the connection string for the database to which a connection will be made.
		/// </summary>
		public IdentityDbContextInt(string connectionString) : base(connectionString) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="IdentityDbContext" /> class using the existing connection to connect to a database, and initializes it from
		/// the given model.  The connection will not be disposed when the context is disposed if contextOwnsConnection is
		/// false.
		/// </summary>
		/// <param name="existingConnection">An existing connection to use for the new context.</param>
		/// <param name="model">The model that will back this context.</param>
		/// <param name="contextOwnsConnection">Constructs a new context instance using the existing connection to connect to a
		/// database, and initializes it from the given model.  The connection will not be disposed when the context is
		/// disposed if contextOwnsConnection is false.
		/// </param>
		public IdentityDbContextInt(DbConnection existingConnection, DbCompiledModel model, bool contextOwnsConnection)
			: base(existingConnection, model, contextOwnsConnection)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="IdentityDbContext" /> class using conventions to create the name of
		/// the database to which a connection will be made, and initializes it from
		/// the given model.  The by-convention name is the full name (namespace + class
		/// name) of the derived context class.  See the class remarks for how this is
		/// used to create a connection.
		/// </summary>
		/// <param name="model">The model that will back this context.</param>
		public IdentityDbContextInt(DbCompiledModel model)
			: base(model)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="IdentityDbContext" /> class using the existing connection to connect
		/// to a database.  The connection will not be disposed when the context is disposed
		/// if contextOwnsConnection is false.
		/// </summary>
		/// <param name="existingConnection">An existing connection to use for the new context.</param>
		/// <param name="contextOwnsConnection">If set to true the connection is disposed when the context is disposed, otherwise
		/// the caller must dispose the connection.
		/// </param>
		public IdentityDbContextInt(DbConnection existingConnection, bool contextOwnsConnection)
			: base(existingConnection, contextOwnsConnection)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="IdentityDbContext" /> class using the connection
		/// string for the database to which a connection will be made, and initializes
		/// it from the given model.  See the class remarks for how this is used to create
		/// a connection.
		/// </summary>
		/// <param name="connectionString">Connection string.</param>
		/// <param name="model">The model that will back this context.</param>
		public IdentityDbContextInt(string connectionString, DbCompiledModel model)
			: base(connectionString, model)
		{
		}
	}

	/// <summary>
	/// Base class for the Entity Framework database context used for identity.
	/// </summary>
	/// <typeparam name="TUser">The type of the user objects.</typeparam>
	public class IdentityDbContextInt<TUser> : IdentityDbContext<TUser, IdentityRoleInt, int, IdentityUserLoginInt, IdentityUserRoleInt, IdentityUserClaimInt, IdentityRoleClaimInt>
		where TUser : IdentityUserInt
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="IdentityDbContext" /> class using the connection string for the database to which a connection will be made.
		/// </summary>
		public IdentityDbContextInt(string connectionString) : base(connectionString) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="IdentityDbContext" /> class using the existing connection to connect to a database, and initializes it from
		/// the given model.  The connection will not be disposed when the context is disposed if contextOwnsConnection is
		/// false.
		/// </summary>
		/// <param name="existingConnection">An existing connection to use for the new context.</param>
		/// <param name="model">The model that will back this context.</param>
		/// <param name="contextOwnsConnection">Constructs a new context instance using the existing connection to connect to a
		/// database, and initializes it from the given model.  The connection will not be disposed when the context is
		/// disposed if contextOwnsConnection is false.
		/// </param>
		public IdentityDbContextInt(DbConnection existingConnection, DbCompiledModel model, bool contextOwnsConnection)
			: base(existingConnection, model, contextOwnsConnection)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="IdentityDbContext" /> class using conventions to create the name of
		/// the database to which a connection will be made, and initializes it from
		/// the given model.  The by-convention name is the full name (namespace + class
		/// name) of the derived context class.  See the class remarks for how this is
		/// used to create a connection.
		/// </summary>
		/// <param name="model">The model that will back this context.</param>
		public IdentityDbContextInt(DbCompiledModel model)
			: base(model)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="IdentityDbContext" /> class using the existing connection to connect
		/// to a database.  The connection will not be disposed when the context is disposed
		/// if contextOwnsConnection is false.
		/// </summary>
		/// <param name="existingConnection">An existing connection to use for the new context.</param>
		/// <param name="contextOwnsConnection">If set to true the connection is disposed when the context is disposed, otherwise
		/// the caller must dispose the connection.
		/// </param>
		public IdentityDbContextInt(DbConnection existingConnection, bool contextOwnsConnection)
			: base(existingConnection, contextOwnsConnection)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="IdentityDbContext" /> class using the connection
		/// string for the database to which a connection will be made, and initializes
		/// it from the given model.  See the class remarks for how this is used to create
		/// a connection.
		/// </summary>
		/// <param name="connectionString">Connection string.</param>
		/// <param name="model">The model that will back this context.</param>
		public IdentityDbContextInt(string connectionString, DbCompiledModel model)
			: base(connectionString, model)
		{
		}
	}
}
