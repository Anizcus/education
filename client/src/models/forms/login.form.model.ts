interface LoginFormModel {
  username: string;
  password: string;
}

const LoginFormRules = {
  username: [
    { required: true, message: "Please input username", trigger: "blur" }
  ],
  password: [
    { required: true, message: "Please input password", trigger: "blur" }
  ]
};

export { LoginFormModel, LoginFormRules };
