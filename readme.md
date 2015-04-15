## joestrommen.com

This is the source code for my entire site.  I'm intentionally developing this in the open.  Maybe somebody else will find this useful, and at the very least I will find it useful if (when) my laptop dies.

### IIS Configuration
Add a new Website `local.joestrommen.com`, with the physical path as the joestrommen.com folder within the solution directory (e.g. `c:\dev\joestrommen.com\joestrommen.com`)

You'll also need the IIS Rewrite module, which is available through the Web Platform Installer.

### DNS Configuration
You'll need to map `local.joestrommen.com` to your localhost.  The easiest way is to add the following line to your HOSTS file (c:\Windows\System32\drivers\etc\hosts):
>	127.0.0.1	local.joestrommen.com
 
### Node/IO.js Configuration

The site uses Grunt (running under Node) to do some front-end optimizations.  To get this to work you'll need to do a `npm install` to install Grunt into the working folder.

You'll also need to do a `npm install -g grunt-cli` if you haven't already so that `grunt` can be run from the command line.

### Non-Public Data
There are a few things that I'm not going to commit to Github.

* MailChimpApiKey
* MandrillApiKey
* Mandrill SMTP password
* Azure Deployment credentials