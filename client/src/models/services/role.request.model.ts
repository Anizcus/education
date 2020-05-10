interface RoleRequestModel {
   forRegistration: boolean;
}

interface ModifyUserRequestModel {
   userId: number;
   isBlocked: boolean;
   roleId: number;
}

export { RoleRequestModel, ModifyUserRequestModel }