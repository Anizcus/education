interface SessionModel {
  id: number;
  name: string;
}

interface UserStoreModel {
  session?: SessionModel;
}

export { UserStoreModel, SessionModel };
