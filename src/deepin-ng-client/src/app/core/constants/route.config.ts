const ACOCUNT_ROUTERS = {
    root: 'account',
    register: "sign-up",
    registerConfirmation: "sign-up-confirmation",
    login: "sign-in",
    forgotPassword: 'forgot-password',
    resetPassword: 'reset-password',
}

const HOME_ROUTERS = {
    root: 'home'
}

const PAGE_ROUTERS = {
    root: 'page',
    new: 'new',
    edit: 'edit/:id',
    detail: ':/id'
}

const NOTES_ROUTERS = {
    root: 'notes',
    new: 'new',
    edit: 'edit/:id'
}
export {
    ACOCUNT_ROUTERS,
    HOME_ROUTERS,
    PAGE_ROUTERS,
    NOTES_ROUTERS
}